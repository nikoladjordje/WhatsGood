import { useState, useEffect } from "react";
import FriendRequest from "./FriendRequest";

const FriendRequests = (props) => {
    const [friendRequests, setFriendRequests] = useState([]);
    const [refetch, setRefetch] = useState(false);

    useEffect(() => {
        const fetchFriendRequests = async () => {
          const response = await fetch('https://localhost:7209' + '/FriendRequest/GetAllFriendRequests?username=' + props.user.username, {
                  headers: { 'Content-Type': 'application/json' },
                  credentials: 'include'
                });
          const data = await response.json();
          console.log(data);
          setFriendRequests(data);
        };
        
        fetchFriendRequests();
        props.setFetchFriends(!props.fetchFriends);
        console.log('friend requests');
      }, [refetch]);

    return (
        <>
            {friendRequests.map((friendRequest, id) => {
              if(!friendRequest.isAccepted)
                return <FriendRequest friendRequest={friendRequest} key={id} user={props.user} setRefetch={setRefetch} refetch={refetch}/>
            })}
        </>
    );
}

export default FriendRequests;