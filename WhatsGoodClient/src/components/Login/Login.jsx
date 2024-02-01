import { useState } from "react";
import Cookies from 'js-cookie';
import Register from "./Register";

const Login = (props) => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const [logIn, setLogIn] = useState(true);

    const submit = async (e) => {
        e.preventDefault();

        const response = await fetch('https://localhost:7209' + '/User/Login', {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            credentials: 'include',
            body: JSON.stringify({
                email,
                password
            }), 
            mode:"cors"
        });

        if (!response.ok) {
            props.setLoggedIn(false);
        }
        else{
            
            const content = await response.json();
            
            console.log(content);
            props.setLoggedIn(true);
            const jwt = Cookies.get('jwt');
        }

    };
    
    return (
        <>
        <div className="d-flex flex-row mx-auto w-40 justify-content-center ">
            <div className="m-3 p-3 bg-light rounded" onClick={() => setLogIn(true)}>Sign in</div>
            <div className="m-3 p-3 bg-light rounded" onClick={() => setLogIn(false)}>Sign up</div>
        </div>
        {logIn &&<div className="w-50 mx-auto">
            <h2>Login</h2>
            <form onSubmit={submit}>
                <div className="mb-3">
                    <input type="text" className="form-control" placeholder="email" required onChange={e => setEmail(e.target.value)}/>
                </div>
                <div className="mb-3">
                    <input type="password" className="form-control" placeholder="password" required onChange={e => setPassword(e.target.value)}/>
                </div>
                <button className="btn btn-primary w-100 py-2" type="submit">Sign in</button>
            </form>

        </div>}
        {!logIn && 
            <Register setLogIn={setLogIn}/>
            }
        
        </>
    );
};

export default Login;