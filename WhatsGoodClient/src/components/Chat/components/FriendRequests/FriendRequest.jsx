const FriendRequest = (props) => {
    const handleAccept = async () => {
        const response = await fetch('https://localhost:7209' + `/FriendRequest/AcceptFriendRequest?requestId=${props.friendRequest.id}&userId=${props.user.id}`, {
        method: 'POST',
        headers: {'Content-Type': 'application/json'},
            credentials: 'include'
        });
        props.setRefetch(!props.refetch);
        console.log('friend request');
    }

    const handleDecline = async () => {
        const response = await fetch('https://localhost:7209' + `/FriendRequest/DeclineFriendRequest?requestId=${props.friendRequest.id}&userId=${props.user.id}`, {
        method: 'POST',
        headers: {'Content-Type': 'application/json'},
            credentials: 'include'
        });
        props.setRefetch(!props.refetch);
        console.log('friend request');
    }
    
    return(
        <>
        <div>{props.friendRequest.sender.username}</div>
        <div>
            <button className="btn btn-primary" onClick={handleAccept}>Accept</button>
            <button className="btn btn-secondary" onClick={handleDecline}>Decline</button>
        </div>
        </>
    );
}

export default FriendRequest;