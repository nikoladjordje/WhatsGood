const ChatListItem = (props) => {
	const handleClick = () => {
		props.setFriend(props.friend);
		console.log(props.friend);
	}
    return(
        <>
        <div className="card" onClick={handleClick}>
			<div className="card-body p-2">
            	<h5 className="card-title">{props.friend.username}</h5>
			</div>
				
        </div>
        </>
    );
}

export default ChatListItem;