import { useEffect, useState } from "react";
import TypingArea from "./TypingArea/TypingArea";
import * as signalR from '@microsoft/signalr';

const MessageList = (props) => {
    const [messages, setMessages] = useState([]);
    const [newMessage, setNewMessage] = useState('');
    const [connection, setConnection] = useState();
    var sender = '';

    // useEffect(() => {
        
    // }, []);
    
    useEffect(() => {
		if(props.friend){
            const fetchMessages = async () => {
                  const response = await fetch('https://localhost:7209' + '/Message/GetAllMessages?senderId=' + props.user.id + '&recipientId=' + props.friend.id, {
                    headers: { 'Content-Type': 'application/json' },
                    credentials: 'include'
                });
                    
                  const data = await response.json();
                  data.forEach((message) => {
                // console.log(message.sender.name);
                // console.log(message.content);
                if(message.senderId==props.user.id)
                    sender = props.user.username;
                else
                    sender = props.friend.username;
                setMessages(prevMessages => [...prevMessages, `${sender}: ${message.content}`]);
                  });
                  console.log(data);
              
            //   setFriendRequests(data);
            };
            setMessages([]);
            fetchMessages();

        }
	},[props.friend]);

    useEffect(() => {
		const newConnection = new signalR.HubConnectionBuilder()
			.withUrl('https://localhost:7209/chatHub') 
			.build();
	
		newConnection.on('ReceiveMessage', (username, message) => {
			setMessages(prevMessages => [...prevMessages, `${username}: ${message}`]);
		});
	
		newConnection.start()
			.then(() => {
				// Join a group using the usernames of the two users
				newConnection.invoke('JoinGroup', props.user.username);
				newConnection.invoke('JoinGroup', props.friend.username);
				console.log('SignalR connection established');
			})
			.catch((error) => {
				console.error('Error establishing SignalR connection:', error);
			});
	
		setConnection(newConnection);
	
		return () => {
			if (newConnection) {
				// Leave the group when the component unmounts
				newConnection.invoke('LeaveGroup', props.user.username);
				newConnection.invoke('LeaveGroup', props.friend.friendname);
				newConnection.stop();
			}
		};
	}, [props.user, props.friend]);

    const sendMessage = async () => {
        if (connection && newMessage.trim() !== '') {
            try {
                // Invoke the 'SendMessageToUser' method on the server
                await connection.invoke('SendMessageToUser', props.friend.username, props.user.username, newMessage);
                setNewMessage('');
            } catch (error) {
                console.error('Error sending message:', error);
            }
        }
    };

    return (
        <>
        {messages.map((msg, index) => (
			<div key={index}>{msg}</div>
		))}
        <TypingArea setNewMessage={setNewMessage} newMessage={newMessage} sendMessage={sendMessage}/>
        </>
    );
}

export default MessageList;