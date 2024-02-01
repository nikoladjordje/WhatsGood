const SearchResult = (props) => {
    const handleSendFriendRequest = async () => {
        const response = await fetch(`https://localhost:7209/FriendRequest/SendFriendRequest/${props.user.username}/${props.friend.username}`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            credentials: 'include'
        });
        // const response = await fetch('https://localhost:7209' + '/User/Logout', {
        // method: 'POST',
        // headers: {'Content-Type': 'application/json'},
        //     credentials: 'include'
        // });
        if(response.ok){
            const data = await response.json();
            console.log(data);
        }
        else{
            console.log('already sent');;
        }

    }
    return(
        <>
        <div className="d-flex justify-content-center m-3">
            <div className="w-25">
                {props.friend.username}
            </div>
            <div className="w-25">
                <button onClick={handleSendFriendRequest} className="btn btn-success"><i className="bi bi-person-plus-fill"></i> Add Friend</button>
            </div> 
        </div>
        </>
    );
}

export default SearchResult;