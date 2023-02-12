import { map } from 'jquery';
import { Form, FormGroup, Label, ListGroup, ListGroupItem, ListGroupItemHeading, InputGroupText, Button, ListGroupItemText, Col, Row, Container, InputGroup, Input } from 'reactstrap';
import { BsFillTelephoneFill, BsFillTrashFill, BsSearch } from "react-icons/bs";
import { createContact, getContacts, deleteContact, updateContact } from '../../services/contactsservice';
import { useEffect, useState } from 'react';
import MaskedInput from "react-input-mask";

const contacts = [
    {
        name: "Eric Elliot",
        phone: "2225556575"
    },
    {
        name: "Steve Jobs",
        phone: "2204546754"
    },
    {
        name: "Fred Allen",
        phone: "2106579886"
    },
    {
        name: "Steve Wozniak",
        phone: "3436758786"
    },
    {
        name: "Bill Gates",
        phone: "3436549688"
    }
]

export default function Contacts() {

    const [newcontact, setNewContact] = useState({id:0, name: "",surname:"",phonenumber:"" });
    const [dbcontacts, setDbcontacts] = useState([]);
    const [isAddOrModifiedContact, setIsAddOrModifiedContact] = useState(false);
    const [activeItem, setActiveItem] = useState("");
    const [hasbeenadded, setHasBeenAdded] = useState(true);
    const [searchTerm, setSearchTerm] = useState("");
    const [filteredDbContacts, setFilteredDbContacts] = useState([]);

    useEffect(() => {
        
        const fetchData = async () => {
            var result = await getContacts();
            console.log(result);
            setDbcontacts(result);
          }
          if (hasbeenadded) {
            fetchData();
            setHasBeenAdded(false);
          }
    }, [hasbeenadded]);


    const telephoneFormater = (telephone) => {
        return telephone!==null?`${telephone.slice(0, 3)}-${telephone.slice(3, 6)}-${telephone.slice(6)}`:"";
    }

    const SearchBar = () => {
        return (
            <div style={{ display: 'flex' }}>
                <InputGroup>
                    <InputGroupText style={{ backgroundColor: "white" }}>
                        <BsSearch />
                    </InputGroupText>
                    <Input 
                        placeholder="Search for contact by last name..." 
                        name="searchTerm"
                        value={searchTerm}
                        onChange={(e) => handleSearchTerm(e.target.value)}
                        />
                </InputGroup>

            </div>
        );
    }

    const removeContact = async(id) => {
        await deleteContact(id);
        setHasBeenAdded(true);
    }

    const handleClickListItem = async (item) => {
        setActiveItem(item);
        setNewContact({id:item.id,name:item.name,surname:item.surname,phonenumber:item.telephone});
        setIsAddOrModifiedContact(true);
      };

    const phoneBookContact = (contact) => {
        return (
            <ListGroupItem 
                active={activeItem === contact}
                onClick={() => handleClickListItem(contact)}>
                <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                    <div>
                        <ListGroupItemHeading>{contact.name} {contact.surname}</ListGroupItemHeading>
                        <ListGroupItemText>

                            <span style={{ paddingRight: '0.2em' }}><BsFillTelephoneFill size={13} color="#C0C0C0" /></span>
                            <span style={{ color: "#C0C0C0" }}>{telephoneFormater(contact.telephone)}</span >

                        </ListGroupItemText>
                    </div>
                    <div style={{ paddingTop: '1em' }}>
                        <Button color="danger" onClick={()=>removeContact(contact.id)}><BsFillTrashFill /></Button>
                    </div>

                </div>
            </ListGroupItem>
        )
    };

    const addContact = async () => {
        await createContact(newcontact);
        setIsAddOrModifiedContact(false);
        setHasBeenAdded(true);
    }

    const updateCurrentContact = async () => {
        await updateContact(newcontact);
        setIsAddOrModifiedContact(false);
        setHasBeenAdded(true);
    }

    
    function handleSelect(e) {
        setNewContact((prev) => ({
          ...prev,
          [e.target.name]: e.target.name==="phonenumber"? e.target.value.replace(/[^\d]/g, ''): e.target.value,
        }))
      }
    
    const handleSearchTerm = (searchterm) => {
        setSearchTerm(searchterm);
        setFilteredDbContacts(dbcontacts.filter((contact) => contact.surname.toLowerCase().includes(searchTerm.toLowerCase())));
    }

    const enableNewContact = () => { setIsAddOrModifiedContact(!isAddOrModifiedContact); }
    return (


        <div >
            <Container  >
                <Row style={{ paddingTop: '20px' }}>
                    <Col md="10">
                        <h1>Contacts</h1>
                    </Col>
                    <Col md="2" style={{ justifyContent: "right" }}>
                        <Button color='primary' onClick={()=>enableNewContact()}>Add Contact</Button>
                    </Col>
                </Row>
                {isAddOrModifiedContact && 
                <Row style={{ paddingTop: '20px' }}>

                    <Col md="6">
                        <Form>
                            <FormGroup>
                                <Input placeholder="First Name"
                                 name="name" 
                                 value={newcontact.name}
                                 onChange={handleSelect}
                                />
                            </FormGroup>
                            <FormGroup>
                                <Input placeholder="Last Name" 
                                    name="surname"
                                 value={newcontact.surname}
                                 onChange={handleSelect}
                                />
                            </FormGroup>
                            <FormGroup>
                                 <MaskedInput 
                                 name="phonenumber" 
                                   value={newcontact.phonenumber}
                                   onChange={handleSelect}                                    
                                   mask="(999) 999-9999"
                                    placeholder="Enter a phone number"
                                    className="form-control"
                                />
                            </FormGroup>
                            
                            {activeItem ? 
                            <Button color='primary' onClick={()=>updateCurrentContact()}>
                                Update
                            </Button>:
                            <Button color='primary' onClick={()=>addContact()}>
                                Add
                            </Button>}
                        </Form>
                    </Col>
                </Row> }
                <Row style={{ paddingTop: '20px' }}>
                    <Col><SearchBar /></Col>

                </Row>
                <Row style={{ paddingTop: '20px' }}>
                    <Col>
                        <ListGroup>
                            {searchTerm ? filteredDbContacts && filteredDbContacts.map((contact) => { return (phoneBookContact(contact)) })
                            :
                            dbcontacts && dbcontacts.map((contact) => { return (phoneBookContact(contact)) })}  
                        
                        </ListGroup> 
                    </Col>
                </Row>


            </Container>
        </div>
    );
}