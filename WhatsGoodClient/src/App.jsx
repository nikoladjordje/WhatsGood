import { useState, useEffect } from 'react'
import './App.css'
import Login from './components/Login/Login';
import Chat from './components/Chat/Chat';


function App() {
  const [user, setUser] = useState();
  const [loggedIn, setLoggedIn] = useState(false);

  const a = async () => {
    const response = await fetch('https://localhost:7209' + '/User/GetUser', {
      headers: {'Content-Type': 'application/json'},
        credentials: 'include',
        mode: 'cors'
      });
    if (!response.ok) {
      console.error('Failed to fetch user data');
      setUser();
      // setLoggedIn(false);
      return;
    }
    else{
      
      const content = await response.json();
      setUser(content);
      setLoggedIn(true);
    }
  }

  useEffect(() => {
    console.log('app update');
    a();
  },[loggedIn]);

  return (
    <div className="" style={{width:'1000px',height:'750px'}}>
      {!user ? (
          <Login setLoggedIn={setLoggedIn}/>
        ) : (
          <Chat user={user} setUser={setUser} setLoggedIn={setLoggedIn}/>
        )}
    </div>
  )
}

export default App
