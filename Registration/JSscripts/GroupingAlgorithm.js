/*           FullName: document.getElementById("fullname").value,
                Gender: $("#gender").val(),
                Status: $("#famstatus").val(),
                YearsOfEducation: $("#education").val(),
                UserName: $("#username").val(),
                Password: $("#password").val(),
                Residence: $("#residence").val(),
                Phone: $("#phone").val(),
                BirthDate: $("#birth").val(),
                PrefDay1: $("#prefday1").val(),
                PrefHour1: $("#prefhour1").val(),
                PrefHour2: $("#prefhour2").val(),
                Mail: $("#mail").val(),
                City: $("#city").val()

            }*/

/*groupTime: 1-Morning, 2-Noon, 3-Evening*/
/*educationLevel:1-Fine,2-ok,3-smarties*/
var groupTime;
var educationLevel;
var groupDay;


//Checking User Prefhour and convert to groupHour
function CheckHour() {
    groupDay = User.PrefDay1;
    if (User.PrefHour1 >= 1 && User.PrefHour1 <= 9) {
        groupTime = 1;
    }
    else if (User.PrefHour1 >= 10 && User.PrefHour1 <= 17) {
        groupTime = 2;
    }
    else {
        groupTime = 3;
    }
    CheckEducation();
}
//Checking User YearsOfEducation and convert to Group education Level
function CheckEducation() {
    if (User.YearsOfEducation == 1 || User.YearsOfEducation == 2) {
        educationLevel = 1;
    }
    else if (User.YearsOfEducation == 3) {
        educationLevel = 2;
    }
    else {
        educationLevel = 3;
    }

    ajaxCall("GET", "../api/getAllGroup?day=" + groupDay + "&grouptime=" + groupTime+"&education=" + educationLevel, "", SuccessGetAllGroup, ErrorGetAllGroup);
}



function AddUserToGroup() {
    if (choosenGroup[0].Max_Partcipants > choosenGroup[0].Num_Of_Registered) {
        User.Group_Id = choosenGroup[0].Group_Id;
        User.Group_Version = choosenGroup[0].Group_Version;
        ajaxCall("POST", "../api/User", JSON.stringify(User), userAddSuccefully, userNotAdded);
        ajaxCall("PUT", "../api/group/UpdateGroup", JSON.stringify(choosenGroup[0]), UpdateNumSuccess, UpdateNumError);
    }
    else {
        OpenNewGroup();
    }
}

function OpenNewGroup() {
    choosenGroup[0].Group_Version += 1;
    choosenGroup[0].Num_Of_Registered = 1;
    ajaxCall("POST", "../api/group/insertNewGroup", JSON.stringify(choosenGroup[0]), SuccessAddNewGroup, ErrorAddNewGroup)
} /*ajaxCall("POST", "../api/User", JSON.stringify(User), userAddSuccefully, userNotAdded);*/

function UpdateNumSuccess() {
    swal("Updated!", "Group Is Successfully Updated!", "Success");
}
function UpdateNumError() {
    alert("Error update Group");
}
function ErrorAddNewGroup() {
    alert("sorry update failed");
}
function userAddSuccefully(data) {
    swal("Added!", "User Is Successfully Added!", "Success");
}
function userNotAdded(err) {
    alert("Sorryyyyy");
}
function SuccessGetAllGroup(group) {
    choosenGroup = group;
    AddUserToGroup();
}
function ErrorGetAllGroup() {
    alert("Error get all groups from DB");
}
function SuccessAddNewGroup(data) {
    swal("Add a New To Group!", "A New Group is successfully Added!", "Success");
    User.Group_Version = choosenGroup[0].Group_Version;
    User.Group_Id = choosenGroup[0].Group_Id;
    ajaxCall("POST", "../api/User", JSON.stringify(User), userAddSuccefully, userNotAdded);

}
function ErrorAddNewGroup() {
    alert("Error Insert New Group");
}
