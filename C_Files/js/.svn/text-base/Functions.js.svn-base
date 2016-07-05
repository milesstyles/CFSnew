function CheckChanged(chk) {
    var prefix = "ctl00_ctl00_mainContentHolder_mainContentHolder_";
    var x = chk.checked;
    if (x == true) {
        var name = document.getElementById(prefix + "tbFirstName").value + " " + document.getElementById(prefix + "tbLastName").value;
        document.getElementById(prefix + "tbLocName").value = name;
        document.getElementById(prefix + "tbLocationAddress1").value = document.getElementById(prefix + "tbAddress1").value;
        document.getElementById(prefix + "tbLocAddress2").value = document.getElementById(prefix + "tbAddress2").value;
        document.getElementById(prefix + "tbLocCity").value = document.getElementById(prefix + "tbCity").value;
        document.getElementById(prefix + "ddlLocState").value = document.getElementById(prefix + "ddlState").value;
        document.getElementById(prefix + "tbLocZip").value = document.getElementById(prefix + "tbZip").value;
        document.getElementById(prefix + "tbLocPhone").value = document.getElementById(prefix + "tbHomePhone").value;
    }
}

function Count(text, long) {
    var maxLength = new Number(long);
    if (text.length > maxLength) {
        text = text.substring(0, maxLength);
        alert("Only " + long + " characters allowed");
    }
}

function CalculateJobTotals() {
    var prefix = "ctl00_mainContent_";
    var grossIncome = 0;
    var totExpense = 0;
    var officeNet = 0;

    var income = new Array(4);
    var expenses = new Array(5);

    income[0] = document.getElementById(prefix + "tBoxEntTotal").value
    income[1] = document.getElementById(prefix + "tBoxLimoTotal").value
    income[2] = document.getElementById(prefix + "tBoxLocTotal").value;
    income[3] = document.getElementById(prefix + "tBoxAccessoriesTotal").value;

    expenses[0] = document.getElementById(prefix + "tBoxDancerPayroll").value;
    expenses[1] = document.getElementById(prefix + "tBoxGratuity").value;
    expenses[2] = document.getElementById(prefix + "tBoxSecPayroll").value;
    expenses[3] = document.getElementById(prefix + "tBoxReferCommission").value;
    expenses[4] = document.getElementById(prefix + "tBoxSalesCommission").value;

    /* Add up Income */
    for (i = 0; i < 4; i++) {
        var num = new Number(income[i]);

        if (isNaN(num)) {
            grossIncome = "???";
            break;
        }

        grossIncome += num;
    }

    /* Add up Expenses */
    for (i = 0; i < 5; i++) {
        var num = new Number(expenses[i]);

        if (isNaN(num)) {
            totExpense = "???";
            break;
        }

        totExpense += num;
    }

    if (isNaN(grossIncome) || isNaN(totExpense)) {
        officeNet = "???";
    }
    else {
        officeNet = grossIncome - totExpense;
    }

    document.getElementById(prefix + "tBoxGrossIncome").value = grossIncome;
    document.getElementById(prefix + "tBoxTotalExpenses").value = totExpense;
    document.getElementById(prefix + "tBoxOfficeNet").value = officeNet;
}