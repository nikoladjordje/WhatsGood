import React, { useState } from 'react';

const UpdateAccount = (props) => {
    const [id, setId] = useState(props.user.id);
    const [username, setUsername] = useState(props.user.username);
    const [name, setName] = useState(props.user.name);
    const [lastName, setLastName] = useState(props.user.lastName);
    const [email, setEmail] = useState(props.user.email);

    const handleSubmit = async (e) => {
        e.preventDefault();

        const response = await fetch(`https://localhost:7209/User/UpdateProfile`, {
            method: 'PUT',
            headers: {'Content-Type': 'application/json'},
            credentials: 'include',
            mode:"cors",
            body: JSON.stringify({
                id,
                username,
                name,
                lastName,
                email
            })
        });
        props.setUpdateAccount(false);

    };

    const handleGoBackClick = () => {
        props.setUpdateAccount(false);
    } 

    return (
        <>
        <h3>Update Account</h3>
        <form onSubmit={handleSubmit}>
        <div className="mb-3">
            <label htmlFor="username" className="form-label">Username</label>
            <input type="text" className="form-control" id="username" value={username} onChange={(e) => setUsername(e.target.value)} />
        </div>
        <div className="mb-3">
            <label htmlFor="name" className="form-label">Name</label>
            <input type="text" className="form-control" id="name" value={name} onChange={(e) => setName(e.target.value)} />
        </div>
        <div className="mb-3">
            <label htmlFor="lastName" className="form-label">Last Name</label>
            <input type="text" className="form-control" id="lastName" value={lastName} onChange={(e) => setLastName(e.target.value)} />
        </div>
        <div className="mb-3">
            <label htmlFor="email" className="form-label">Email</label>
            <input type="email" className="form-control" id="email" value={email} onChange={(e) => setEmail(e.target.value)} />
        </div>

        <button onClick={handleGoBackClick} className='btn btn-light m-3'><i className="bi bi-arrow-return-left"></i> Go back</button>
        <button type="submit" className="btn btn-primary m-3">Update Account</button>
        </form>
        </>
    );
}

export default UpdateAccount;