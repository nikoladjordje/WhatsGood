
const TypingArea = (props) => {
    const handleOnSend = async() => {
        await props.sendMessage();
        props.setNewMessage('');
    }
    return (
        <>
        <div className="d-flex flex-row justify-content-center">
            <div className="input w-70 m-2">
                <input type="text" 
                className="form-control"
                value={props.newMessage}
                onChange={(e) => props.setNewMessage(e.target.value)}
			  placeholder="Type your message..." />           
            </div>
            <div className="send-button m-2">
                <button onClick={handleOnSend} className="btn btn-primary"><i className="bi bi-send"></i>Send</button>
            </div>
        </div>
        </>
    );
}

export default TypingArea;