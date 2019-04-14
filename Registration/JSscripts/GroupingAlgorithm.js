
/*groupTime: 1-Morning, 2-Noon, 3-Evening*/
/*educationLevel:1-Fine,2-ok,3-smarties*/
var userToDb;

function RegisterNewUser() {

    ajaxCall("GET", "../api/group/NewAlgoritem?prefday=" + User.PrefDay1 + "&prefhour=" + User.PrefHour1 + "&education=" + User.YearsOfEducation, "", SuccessGettingGroups, ErrorGettingGroups)
}

function SuccessGettingGroups(groupToInsert) {
    console.log("Success");
    User["Group"] = groupToInsert;
    userToDb = JSON.stringify(User);

    AddNewUser();
    
}

function AddNewUser() {
    ajaxCall("POST", "../api/temp", userToDb, SuccessAddingUserToGroup, ErrorAddingUserToGroup);
}
function ErrorGettingGroups() {
    console.log("Error");
}

function SuccessAddingUserToGroup(data) {
    console.log("Success adding user to group");
    Swal.fire({
        title: 'Success',
        text: 'Added a New User',
        imageUrl: "../Images/success.png",
        imageWidth: 400,
        imageHeight: 200,
        imageAlt: 'Custom image',
        animation: false
    });
}
function ErrorAddingUserToGroup() {
    console.log("Error adding user to group");
}


