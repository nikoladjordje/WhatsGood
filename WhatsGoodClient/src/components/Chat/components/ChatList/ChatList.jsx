import { useState, useEffect} from "react";
import ChatListItem from "./ChatListItem/ChatListItem";

const ChatList = (props) => {
    // const [friends, setFriends] = useState();
    // const fetchData = async () => {
    //     // console.log(props.user);

    //     const response = await fetch('https://localhost:7209/' + 'Friendship/GetAllFriends?username=' + props.user.username, {
    //       headers: { 'Content-Type': 'application/json' },
    //       credentials: 'include'
    //     });

    //     if (!response.ok) {
    //       console.error('Failed to fetch friends list');
    //       return;
    //     }

    //     const content = await response.json();
    //     setFriends(content);
    //     console.log(content)
    // };
    // useEffect(() => {
    //     console.log('init fetch');
    //     fetchData();
    // }, []);

    // useEffect(() => {
    //     console.log('refetchem all');
    //     fetchData();
    // }, [props.fetchFriends]);

    return (
        <>
            <div className="">
                {props.friends &&
                props.friends.map((friend, id) => (
                <ChatListItem friend={friend} key={id} setFriend={props.setFriend}/>
                ))}
            </div>
            
        </>
    );

}

export default ChatList