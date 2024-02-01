import { useState } from "react";

const Register = (props) => {
    const [name, setName] = useState('');
    const [lastName, setLastName] = useState('');
    const [username, setUserName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const submit = async (e) => {
        e.preventDefault();

        await fetch('https://localhost:7209' + '/User/Register', {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify({
                username,
                name,
                lastName,
                email,
                password,
            })
        });

        props.setLogIn(true);
    }

    return (
        <>
        <div className="w-50 mx-auto">
            <h2>Register</h2>
            <form onSubmit={submit}>
                    <div className="mb-3">
                        {/* <label htmlFor="name" className="form-label">Name</label> */}
                        <input type="text" className="form-control" id="name" placeholder="Name" required onChange={e => setName(e.target.value)} />
                    </div>
                    <div className="mb-3">
                        {/* <label htmlFor="lastName" className="form-label">Last Name</label> */}
                        <input type="text" className="form-control" id="lastName" placeholder="Last Name" required onChange={e => setLastName(e.target.value)} />
                    </div>
                    <div className="mb-3">
                        {/* <label htmlFor="username" className="form-label">Username</label> */}
                        <input type="text" className="form-control" id="username" placeholder="Username" required onChange={e => setUserName(e.target.value)} />
                    </div>
                    <div className="mb-3">
                        {/* <label htmlFor="email" className="form-label">Email</label> */}
                        <input type="text" className="form-control" id="email" placeholder="Email" required onChange={e => setEmail(e.target.value)} />
                    </div>
                    <div className="mb-3">
                        {/* <label htmlFor="password" className="form-label">Password</label> */}
                        <input type="password" className="form-control" id="password" placeholder="Password" required onChange={e => setPassword(e.target.value)} />
                    </div>
                    <button className="btn btn-primary w-100 py-2" type="submit">Sign up</button>
                </form>

        </div>
        </>
    );
}

export default Register;