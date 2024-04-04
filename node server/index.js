// import { Server } from "socket.io";
// const port = 3000;
// const io = new Server(port);

// io.on("connection", (socket)=>{
//     console.log("Client Connected!");

//     socket.on("test", (data)=>{
//         console.log("Client sent -->", data);

//         var test = {text: "Hi"};
//         socket.emit("test-back", test);
//     })

//     socket.on("disconnect", (data)=>{
//         console.log("Client Disconnected!");
//     })
// })

// console.log("Server Listening at :", port);

import { Server } from "socket.io";
const port = 3000;
const io = new Server(port);

io.on("connection", (socket) => {
  console.log("Client Connected!");

  // Dummy data for position and rotation
  const positions = [
    { x: 8, y: 0, z: 0 },
    { x: 0, y: 4, z: 0 },
    { x: -8, y: 0, z: 0 },
  ];

  const rotations = [
    { x: 0, y: 0, z: 0, w: 0 },
    { x: 0, y: 45, z: 0, w: 0 },
    { x: 0, y: 90, z: 0, w: 0 },
  ];

  let currentIndex = 0;

//   setInterval(() => {
//     socket.emit("movementData", {
//       position: {
//         x: positions[currentIndex].x,
//         y: positions[currentIndex].y,
//         z: positions[currentIndex].z,
//       },
//       rotation: {
//         x: rotations[currentIndex].x,
//         y: rotations[currentIndex].y,
//         z: rotations[currentIndex].z,
//         w: rotations[currentIndex].w,
//       },
//     });

//     currentIndex = (currentIndex + 1) % positions.length;
//   }, 5000);

  setInterval(() => {
    socket.emit("move", {
      x: positions[currentIndex].x,
      y: positions[currentIndex].y,
      z: positions[currentIndex].z,
    });

    currentIndex = (currentIndex + 1) % positions.length;
  }, 1000);

  socket.on("disconnect", (data) => {
    console.log("Client Disconnected!");
  });
});

console.log("Server Listening at :", port);
