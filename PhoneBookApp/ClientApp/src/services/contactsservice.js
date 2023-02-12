export const createContact = async (contact) => {
    try {
        let response = await fetch(`/contacts`,
            {
                method: "POST",
                body: JSON.stringify(contact),
                headers: {
                    "Accept": "application/json",
                    "Content-Type": "application/json",
                }
            }
        );
        let data = await response.json();
        return data;
    }
    catch (error) {
        return -2;
    }
}

export const getContacts = async () => {
    try {
        let response = await fetch(`/contacts`);
        let data = await response.json();
        return data;
    }
    catch (error) {
        return -2;
    }
}

export const deleteContact = async (id) => {
    try {
        let response = await fetch(`/contacts/${id}`,{
            method: "DELETE",
        });
        let data = await response.json();
        return data;
    }
    catch (error) {
        return -2;
    }
}

export const updateContact = async (contact) => {
    try {
        let response = await fetch(`/contacts`,
            {
                method: "PUT",
                body: JSON.stringify(contact),
                headers: {
                    "Accept": "application/json",
                    "Content-Type": "application/json",
                }
            }
        );
        let data = await response.json();
        return data;
    }
    catch (error) {
        return -2;
    }
}
