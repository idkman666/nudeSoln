import React, { useEffect, useState } from 'react';
import "bootstrap/dist/css/bootstrap.min.css";
import HeaderTile from './HeaderTile';
import ItemTile from './ItemTile';
import Form from 'react-bootstrap/Form';
import Stack from 'react-bootstrap/Stack';
import Button from 'react-bootstrap/Button';
import { v1 as uuidv1 } from 'uuid';
import axios from 'axios';
import Spinner from 'react-bootstrap/Spinner';

export default function HomePage() {
    //load items
    const [itemCollection, setItemCollection] = useState({});
    const [itemName, setItemName] = useState("");
    const [itemCategory, setItemCategory] = useState("1");
    const [itemPrice, setItemPrice] = useState(0);
    const [collectionId, setCollectionId] = useState("");
    const [categoriesMap, setCategoriesMap] = useState({});
    const [categories, setCategories] = useState([]);
    const [isDataDirty, setIsDataDirty] = useState(false);
    const [categorizedTotal, setCategorizedTotal] = useState({})
    const [totalPrice, setTotalPrice] = useState(0);
    const [userId, setUserId] = useState("");
    const [isLoading, setIsLoading] = useState(true)


    useEffect(() => {

        let getCategory = async () => {
            let categoryUrl = "/items/getAllCategories";
            const data = await fetch(categoryUrl);
            const jsonData = await data.json();
            const categoriesMap = {};
            setCategories(jsonData);
            jsonData.forEach((element) => {
                categoriesMap[element.id] = element.categoryName;
            });
            setCategoriesMap(categoriesMap);
        }
        let getItemCollections = async () => {

            const res = await axios.get('https://geolocation-db.com/json/')
            let userId = res.data.IPv4;

            let itemCollecitonUrl = "/items/getAllItemCollection/" + userId;
            const data = await fetch(itemCollecitonUrl);
            const jsonData = await data.json();
            if (jsonData.categoryItemsMap === null || Object.keys(jsonData.categoryItemsMap).length === 0) {
                setIsLoading(false);
            }
            setItemCollection(jsonData.categoryItemsMap);
            setCollectionId(jsonData.collectionId);
            setUserId(userId);
           
        }
        getCategory();
        getItemCollections();
        calculateTotal();


    }, [])

    useEffect(() => {
        calculateTotal();
        if ( itemCollection !== undefined && itemCollection !== null && Object.keys(itemCollection).length > 0) {
            setIsLoading(false);
        }
        
    }, [itemCollection])

    //handles adding to list in front end
    function handleSubmit(e) {
        e.preventDefault();
        let item = {
            "itemId": uuidv1(),
            "itemName": itemName,
            "price": parseFloat(itemPrice)
        }

        //check if category key exists in existing itemCollection
        if (itemCollection !== null && itemCategory in itemCollection) {
            //if exists, add to existing key
            setItemCollection({ ...itemCollection, [itemCategory]: [...itemCollection[itemCategory], item] });
            calculateTotal();
            setIsDataDirty(true);
        } else {
            //if it doesn't, new category added
            setItemCollection({ ...itemCollection, [itemCategory]: [item] });
            calculateTotal();
            setIsDataDirty(true);
        }

    }

    //save to database
    const handleSave = async (e) => {
        //if collectionId exists - update
        //if collectionId is undefined - add data
        //if item collection is null - delete
        e.preventDefault();
        let url = "";
        let method = "POST";

        if (collectionId !== null && collectionId !== "" && collectionId !== undefined) {
            //update
            method = "PATCH";
            url = "/items/updateItemCollection/" + userId + "/" + collectionId;

        } else if (collectionId === "" || collectionId === null || collectionId === undefined) {
            //create unique id for the user     
            //add a fresh data to db         
            url = "/items/addItemCollection/" + userId;
        }
        else if (itemCollection === null || itemCollection === undefined || Object.keys(itemCollection).length === 0) {
            //delete item collection            
            url = "/items/deleteItemCollection/" + collectionId;
        }


        let data = JSON.stringify(itemCollection);
        let response = await fetch(url, {
            method: method,
            body: data,
            headers: {
                "Content-Type": "application/json",
                // 'Content-Type': 'application/x-www-form-urlencoded',
            }
        }).catch((e) => alert("Error saving data"));
        if (response.ok) {
            
            //set collectionID only if it is not a PATCH
            if (method === "POST") {
                var jsonData = await response.json();
                setCollectionId(jsonData.collectionId);                
            }
            alert("Data Saved!");
            setIsDataDirty(false);
        } else if (!response.ok) {
            alert("Data Save FAILED!");
        }
    }


    function handleItemNameChange(e) {
        setItemName(e.target.value);
    }

    function handleCategoryChange(e) {
        setItemCategory(e.target.value);
    }

    function handleItemPriceChange(e) {
        setItemPrice(e.target.value);
    }

    const handleItemDelete = (id, categoryKey) => {

        if (itemCollection !== undefined) {
            const filteredList = itemCollection[categoryKey].filter((item) => item.itemId !== id);
            if (filteredList.length === 0) {
                //no data for the category
                //delete category from item collection
                delete itemCollection[categoryKey];
                setItemCollection({ ...itemCollection });

            } else {
                setItemCollection({ ...itemCollection, [categoryKey]: filteredList });
            }
            calculateTotal();
            setIsDataDirty(true);

        }

    }

    const calculateTotal = () => {
        if (itemCollection !== null && itemCollection !== undefined) {
            let mappedTotal = Object.keys(itemCollection).map((key, index) => itemCollection[key].reduce((total, currentValue) => total = total + currentValue.price, 0));
            let categoryTotal = {};
            Object.keys(itemCollection).map((key, index) => categoryTotal[key] = mappedTotal[index]);
            setCategorizedTotal(categoryTotal);
            setTotalPrice(mappedTotal.reduce((v, c) => v + c, 0));

        }
    }


    function ListItem() {

        if (itemCollection === undefined || itemCollection === null || Object.keys(itemCollection).length === 0) {
            return <div>
                <h1 style={{ margin: "auto", width: "200px" }}>No Data</h1>
            </div>;
        } else if (Object.keys(itemCollection).length > 0) {
            return (<div>
                {
                    Object.keys(itemCollection).map((key, index) => <li key={index}><HeaderTile name={categoriesMap[key]} price={categorizedTotal[key]} ></HeaderTile> {
                        itemCollection[key].map((item, index) => <ItemTile key={index} name={item.itemName} price={item.price} categoryKey={key} id={item.itemId} handleDelete={handleItemDelete} ></ItemTile>)
                    }</li>
                    )
                }
            </div>);
        }
    }




    return (

        <div className="Row" >
            <h1>Nude Solutions</h1>
            <div className="card" style={{ maxWidth: "700px", margin: "auto", backgroundColor:"grey" }}>

                <ul className="listview" style={{ listStyle: "none" }}>
                    {
                        isLoading === true ? <div style={{width:"30px", margin:"auto"}}><Spinner animation="border" role="status">
                            <span className="visually-hidden">Loading...</span>
                        </Spinner></div> : <ListItem />
                    }
                    <HeaderTile price={totalPrice} name="Total"></HeaderTile>
                </ul>                
                
                <Form onSubmit={handleSubmit} className="rounded p-4">
                    <Stack direction="horizontal" gap={3}>
                        <input className="w-25 form-control" type="text" placeholder="Item Name" onChange={handleItemNameChange} required></input>

                        <input className="w-25 form-control" type="number" placeholder="Item Price" min="0" onChange={handleItemPriceChange} required></input>
                        <select className="form-select w-50" name="Category" style={{ width: "60px" }} onChange={handleCategoryChange} >
                            {
                                categories.map((category, index) => <option value={category.id} key={index} >{category.categoryName}</option>)
                            }
                        </select>
                        <Button variant="primary" type="submit" onSubmit={handleSubmit} > Add </Button>
                    </Stack>

                </Form>
                {
                    isDataDirty ? <Button variant="success" type="submit" onClick={handleSave} style={{ width: "100%" }}> SAVE</Button> : <a></a>
                }


            </div>

        </div>
    );

}
