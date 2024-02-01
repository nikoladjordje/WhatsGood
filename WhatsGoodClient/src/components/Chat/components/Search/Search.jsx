import { useEffect, useState } from "react";
import SearchResult from "./SearchResult";

const Search = (props) => {
    const [find, setSearch] = useState('');
    const [results, setResults] = useState([]);

    const searching = props.user.username;
    
    useEffect(() => {
        const search = async () => {
          if (find.trim() === '') {
            setResults([]);
            return;
          }
    
          const response = await fetch(`https://localhost:7209/User/Search/${encodeURIComponent(find)}/${encodeURIComponent(searching)}`, {
            method: 'GET',
            headers: {
              'Content-Type': 'application/json',
              'credentials': 'include'
            },
          });
    
          const users = await response.json();
    
          if (response.ok) {
            setResults(users);
          }
        };
    
        search();
      }, [find, searching]);

    const handleChange = (value) => {
        setSearch(value);
      }

    const handleGoBackClick = () => {
        props.setSearch(false);
    } 
    
    return (
        <>
        <div className="d-flex flex-column" role="search">
            <input className="form-control me-2" placeholder="Search" value={find} onChange={e => handleChange(e.target.value)} />
            <div className="search-results">
                {results.map((result, id) => {
                    return <SearchResult user={props.user} friend={result} key={id} />
                })}
            </div>
            <button onClick={handleGoBackClick} className='btn btn-light m-3'><i className="bi bi-arrow-return-left"></i> Go back</button>
        </div>
        </>
    );
}

export default Search;