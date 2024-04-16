import { Server } from "socket.io";
const port = 3000;
const io = new Server(port);

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

  //For autonomous movement
  let currentIndex = 0;
  setInterval(() => {
    socket.emit("translate", {
      position: translations[currentIndex].position,
      rotation: translations[currentIndex].rotation,
    });

    currentIndex = (currentIndex + 1) % translations.length;
  }, 1500);

  //Data from player movement
  socket.on("player-move", (data)=>{
      console.log("Player move -->", data);
      
      // Emit "mirror" event with the same data received
      socket.emit("mirror", data);
  })

  socket.on("disconnect", (data) => {
    console.log("Client Disconnected!");
  });
});

console.log("Server Listening at :", port);
