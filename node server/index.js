import { Server } from "socket.io";
const port = 3000;
const io = new Server(port);

io.on("connection", (socket)=>{
    console.log("Client Connected!");

    socket.on("test", (data)=>{
        console.log("Client sent -->", data);

        var test = {text: "Hi"};
        socket.emit("test-back", test);
    })
})

console.log("Server Listening at :", port);