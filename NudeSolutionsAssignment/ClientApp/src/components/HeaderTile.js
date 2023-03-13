import Stack from 'react-bootstrap/Stack';
export default function HeaderTile(props) {

    return (
        <div style={{ marginLeft: "-20px", width:"90%" }}>
            <Stack className="mt-3" direction="horizontal" gap={3} style={{ marginBottom: "0px" }}>   
            <h3 className="me-auto card-text">{props.name}</h3>
            <h3 className="ms-1">${ props.price}</h3>    
        </Stack>
        </div>
        
    );

}