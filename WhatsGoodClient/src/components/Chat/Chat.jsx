import ChatList from "./components/ChatList/ChatList";
import MessageList from "./components/MessageList/MessageList";
import { useState, useEffect } from 'react'
import UpdateAccount from "./components/UpdateAccount/UpdateAccount";
import Search from "./components/Search/Search";
import FriendRequests from "./components/FriendRequests/FriendRequests";

const Chat = (props) => {
  const [friend, setFriend] = useState();
  const [updateAccount, setUpdateAccount] = useState(false);
  const [search, setSearch] = useState(false);
  const [fetchFriends, setFetchFriends] = useState(false);
  const [friends, setFriends] = useState();

  const fetchData = async () => {
    // console.log(props.user);

    const response = await fetch('https://localhost:7209/' + 'Friendship/GetAllFriends?username=' + props.user.username, {
      headers: { 'Content-Type': 'application/json' },
      credentials: 'include'
    });

    if (!response.ok) {
      console.error('Failed to fetch friends list');
      return;
    }

    const content = await response.json();
    setFriends(content);
    console.log(content);
  };
  
  useEffect(() => {
     console.log('init fetch');
    fetchData();
  }, []);

  // useEffect(() => {
  //   console.log('refetchem all');
  //   fetchData();
  // }, [fetchFriends]);





  useEffect(() => {
    props.setLoggedIn(true);
    console.log('update');
  }, [updateAccount])

  useEffect(() => {
    setSearch(false);
    setUpdateAccount(false);
  }, [friend]);

  useEffect(()=> {
    console.log('chat');
    setFetchFriends(true);
  }, [fetchFriends]);

  const logout = async (e) => {
    const response = await fetch('https://localhost:7209' + '/User/Logout', {
      method: 'POST',
      headers: {'Content-Type': 'application/json'},
        credentials: 'include'
    });
    // setSearch(false);
    // setUpdateAccount(false);
    if(response.ok){
      // props.setUser(false);
      // console.log(props.user);
      props.setLoggedIn(false);

    }
    
  }

  const handleAccountClick = () => {
    setSearch(false);
    setUpdateAccount(true);
  }

  const handleSearchClick = () => {
    setUpdateAccount(false);
    setSearch(true);
  }

  return (
    <>
      <div className="container mx-auto p-2 ">
        <div className="row">
          <div className="col-md-4 bg-light p-1">
            <ChatList user={props.user} setFriend={setFriend} fetchFriends={fetchFriends} friends={friends}/>
            <div className="d-flex justify-content-around">
              <button className="btn btn-primary m-2" onClick={handleAccountClick}>
                <i className="bi bi-person-circle"></i> {props.user.username}
              </button>
              <button className="btn btn-secondary m-2"  onClick={handleSearchClick}>
              <i className="bi bi-search"></i> Search
              </button>
              <button onClick={logout} className="btn btn-danger m-2">
                <i className="bi bi-box-arrow-in-left"></i> Logout
              </button>
            </div>
          </div>
          <div className="col-md-8 p-1">
            {updateAccount && 
            <>
            <UpdateAccount user={props.user} setUpdateAccount={setUpdateAccount}/>
            <FriendRequests user={props.user} setFetchFriends={setFetchFriends} fetchFriends={fetchFriends}/>
            </>
            }
            {!updateAccount && !search && friend && <MessageList user={props.user} friend={friend} />}
            {search && <Search user={props.user} setSearch={setSearch}/>}
          </div>
        </div>
      </div>
    </>
  );
}

export default Chat;