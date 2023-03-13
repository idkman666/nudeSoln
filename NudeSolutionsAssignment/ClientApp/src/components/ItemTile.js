import { Component } from 'react';
import Stack from 'react-bootstrap/Stack';
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap-icons/font/bootstrap-icons.css";

export default function ItemTile(props) {
    
    return (<div className="card p-1" style={{ transition: "all 2s" } }>
        <Stack direction="horizontal" gap={ 3} >
            <h6 className="me-auto card-text">{props.name}</h6>
            <div>
                <Stack direction="horizontal" gap={ 6}>
                    <div className=""> <h6 style={{marginRight:"15px"}}>${props.price}</h6></div>
                    <a onClick={() => props.handleDelete(props.id, props.categoryKey)}> <i className="bi bi-trash" style={{fontSize:"20px"} }></i></a>
                </Stack>
            </div>
           
           
        </Stack>
       
    </div>);
    
}