const messageReceived = (msg) => {
    const chatArea = document.getElementById("chatArea");
    chatArea.innerText += msg;
};

const sendMessage = () => {
    const msg = document.getElementById("newMessageInput").value;
    connection.invoke("sendMessage", msg)
};

const joinRoom = () => {
    const roomId = document.getElementById("joinRoomInput").value;
    console.log("roomid" + roomId);
    connection.invoke("joinRoom", roomId);
};

toggleButtonAccess();
const connection = new signalR.HubConnectionBuilder().withUrl('https://localhost:5001/ws/users').build();
establishConnection(connection);
connection.on("sendMessage", messageReceived);



// const getUsers = () => connection.invoke('getUsers');
// const getUser = () => {
//   const xhr = new XMLHttpRequest();
//   xhr.withCredentials = true;
//   xhr.open('GET', 'http://localhost:5000/api/user/my-info-ws')
//   xhr.send();
// }
//
// const startClock = () => connection.stream('TickTock')
//   .subscribe({
//     next: item => console.log(item)
//   });
//
// const stopClock = () => connection.invoke('stopClock');
//
// const sendMessage = () => connection.invoke('broadcastMessage', 'some message');
//
// const throwException = () => connection.invoke('throwException')
//   .then(console.log)
//   .catch(e => console.error(`exception was catched: ${e.toString()}`));
