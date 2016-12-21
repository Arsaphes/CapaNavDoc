function IsLastNameEmpty() {
    if (document.getElementById('LastNameTextBox').value == "") {
        return "Le nom de famille est obligatoire.";
    } else {
        return "";
    }

} 