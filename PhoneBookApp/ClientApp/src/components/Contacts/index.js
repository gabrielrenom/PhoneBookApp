import { map } from 'jquery';
import { Form, FormGroup, Label, ListGroup, ListGroupItem, ListGroupItemHeading, InputGroupText, Button, ListGroupItemText, Col, Row, Container, InputGroup, Input } from 'reactstrap';
import { BsFillTelephoneFill, BsFillTrashFill, BsSearch } from "react-icons/bs";
import { createContact, getContacts, deleteContact } from '../../services/contactsservice';
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

    const [newcontact, setNewContact] = useState({ name: "",surname:"",phonenumber:"" });
    const [dbcontacts, setDbcontacts] = useState([]);
    const [isAddContact, setIsAddContact] = useState(false);

    useEffect(() => {
        const fetchData = async () => {
            var result = await getContacts();
            console.log(result);
            setDbcontacts(result);
          }
        fetchData().catch(console.error);
    }, [dbcontacts]);


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
                    <Input placeholder="Search for contact by last name..." />
                </InputGroup>

            </div>
        );
    }

    const removeContact = (id) => {
        deleteContact(id);
        console.log(id);
    }

    const phoneBookContact = (contact) => {
        return (
            <ListGroupItem>
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

    const addContact = () => {
        createContact(newcontact);
        setIsAddContact(false);
    }

    function handleSelect(e) {
        setNewContact((prev) => ({
          ...prev,
          [e.target.name]: e.target.value,
        }))
      }
    const enableNewContact = () => { setIsAddContact(!isAddContact); }
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
                {isAddContact && 
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
                                {/* <Input placeholder="Telephone"
                                      name="phonenumber" 
                                   value={newcontact.phonenumber}
                                   onChange={handleSelect}
                                   component={MyInput}
                                /> */}
                                 {/* <MaskedInput 
                                 name="phonenumber" 
                                   value={newcontact.phonenumber}
                                   onChange={handleSelect}                                    
                                   mask="(999) 999-9999"
                                    placeholder="Enter a phone number"
                                    className="form-control"
                                /> */}
                            </FormGroup>
                            <Button color='primary' onClick={()=>addContact()}>
                                Add
                            </Button>
                        </Form>
                    </Col>
                </Row> }
                <Row style={{ paddingTop: '20px' }}>
                    <Col><SearchBar /></Col>

                </Row>
                <Row style={{ paddingTop: '20px' }}>
                    <Col>
                        <ListGroup>
                             {dbcontacts && dbcontacts.map((contact) => { return (phoneBookContact(contact)) })}  
                        </ListGroup>
                    </Col>
                </Row>


            </Container>
        </div>
    );
}