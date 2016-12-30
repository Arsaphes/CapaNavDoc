function ValidateRequired(element) {
    var validatorLabel;
    if (element.value === "") {
        validatorLabel = document.getElementById(element.getAttribute("validatorLabelId"));
        validatorLabel.style.display = "inherit";
        validatorLabel.innerHTML = element.getAttribute("message");
        return false;
    } 
    validatorLabel = document.getElementById(element.getAttribute("validatorLabelId"));
    validatorLabel.style.display = "none";
    return true;
}

function ValidateDateFormat(element) {
    var validatorLabel;
    if (element.value === "") {
        validatorLabel = document.getElementById(element.getAttribute("validatorLabelId"));
        validatorLabel.style.display = "inherit";
        validatorLabel.innerHTML = element.getAttribute("message");
        return false;
    }
    if (element.value.match(/^(\d{1,2})-(\d{1,2})-(\d{4})$/) == null) {
        validatorLabel = document.getElementById(element.getAttribute("validatorLabelId"));
        validatorLabel.style.display = "inherit";
        validatorLabel.innerHTML = element.getAttribute("message");
        return false;
    }
    validatorLabel = document.getElementById(element.getAttribute("validatorLabelId"));
    validatorLabel.style.display = "none";
    return true;
}

function Validate() {

    var isValid = true;
    var elems = document.getElementsByTagName("*");

    for (var i = 0; i < elems.length; i++) {
        if (elems[i] == null) {
            continue;
        }
        if (elems[i].id.indexOf("-val") > -1) {
            var items = elems[i].id.split(".");

            switch (items[1]) {
                case "Required":
                    isValid &= ValidateRequired(elems[i]);
                    break;
                case "DateFormat":
                    isValid &= ValidateDateFormat(elems[i]);
                    break;
                default:
                    break;
            }
        }
    }

    if (isValid) {
        return true;
    }
    return false;
}