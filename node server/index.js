import { Server } from "socket.io";
import fs from "fs";
import xlsx from "xlsx";

const port = 3000;
const io = new Server(port);

// Create game_data folder if it doesn't exist
const gameDataFolder = "./game_data";
if (!fs.existsSync(gameDataFolder)) {
  fs.mkdirSync(gameDataFolder);
}

let gameData = [];

const translations = [
  {
    position: { x: 0, y: 0, z: 5 },
    rotation: { x: 0, y: -90, z: 0 },
  },
  {
    position: { x: -5, y: 0, z: 5 },
    rotation: { x: 0, y: -180, z: 0 },
  },
  {
    position: { x: -5, y: 0, z: -5 },
    rotation: { x: 0, y: -270, z: 0 },
  },
  {
    position: { x: 5, y: 0, z: -5 },
    rotation: { x: 0, y: 0, z: 0 },
  },
  {
    position: { x: 5, y: 0, z: 5 },
    rotation: { x: 0, y: -90, z: 0 },
  },
  {
    position: { x: 0, y: 0, z: 5 },
    rotation: { x: 0, y: -180, z: 0 },
  },
  {
    position: { x: 0, y: 0, z: 0 },
    rotation: { x: 0, y: 0, z: 0 },
  },
];

io.on("connection", (socket) => {
  console.log("Client Connected!");

  const startTime = Date.now();

  // Data for autonomous movement
  let currentIndex = 0;
  setInterval(() => {
    socket.emit("translate", {
      position: translations[currentIndex].position,
      rotation: translations[currentIndex].rotation,
    });

    currentIndex = (currentIndex + 1) % translations.length;
  }, 1500);

  // Data from player movement
  socket.on("player-move", (data) => {
    console.log(data);
    const jsonData = JSON.parse(data);

    const timestamp = (Date.now() - startTime) / 1000;

    gameData.push({
      pos_x: jsonData.position.x,
      pos_y: jsonData.position.y,
      pos_z: jsonData.position.z,
      rot_x: jsonData.rotation.x,
      rot_y: jsonData.rotation.y,
      rot_z: jsonData.rotation.z,
      rot_w: jsonData.rotation.w || "",
      timestamp: timestamp,
    });

    socket.emit("mirror", data);
  });

  socket.on("disconnect", (data) => {
    console.log("Client Disconnected!");

    // Save data to Excel file
    const endTime = Date.now();
    const fileName = `game_data_${startTime}_${endTime}.xlsx`;
    const filePath = `${gameDataFolder}/${fileName}`;
    saveGameDataToExcel(gameData, filePath);
    
  });
});

console.log("Server Listening at :", port);

// Function to save game data to Excel
function saveGameDataToExcel(data, filePath) {
  const wb = xlsx.utils.book_new();
  const ws = xlsx.utils.json_to_sheet(data);
  xlsx.utils.book_append_sheet(wb, ws, "Game Data");
  xlsx.writeFile(wb, filePath);
}
