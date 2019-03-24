
/*************************Hours Maker******************/
function createHourList() {
    var hourList1 = "<option value='-1' selected>בחר</option>";
    var hourList2 = "<option value='-1' selected>בחר</option>";

    for (let i = 0; i < hourArr.length; i++) {
        hourList1 += "<option value = '" + (i+1) + "'>" + hourArr[i] + "</option>";
        hourList2 += "<option value = '" + (i + 1) + "'>" + hourArr[i] + "</option>";
    }
    document.getElementById("prefhour1").innerHTML = hourList1;
    document.getElementById("prefhour2").innerHTML = hourList1;

}
/*************************Ajax User Registration******************/

/*************************Auto Fill Script******************/
function autofillScript() {
    document.getElementById("password").value = "passwordForTest";
    document.getElementById("username").value = "geeeveeer";
    document.getElementById("city").value = 1;
    document.getElementById("mail").value = "rrrrrr@gmail.com";
    document.getElementById("phone").value = "0987654321";
    document.getElementById("fullname").value = "test name";
}