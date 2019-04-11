
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
    ajaxCall("GET", "../api/group/getAllGroup?day=" + groupDay + "&grouptime=" + groupTime+"&education=" + educationLevel, "", SuccessGetAllGroup, ErrorGetAllGroup);
}

function AddUserToGroup() {
    if (choosenGroup[0].Max_Partcipants > choosenGroup[0].Num_Of_Registered) {
        User.Group_Id = choosenGroup[0].Group_Id;
        User.Group_Version = choosenGroup[0].Group_Version;
        ajaxCall("POST", "../api/User", JSON.stringify(User), userAddSuccefully, userNotAdded);
        
    }
    else {
        OpenNewGroup();
    }
}

function OpenNewGroup() {
    choosenGroup[0].Group_Version += 1;
    choosenGroup[0].Num_Of_Registered = 0;
    ajaxCall("POST", "../api/group/insertNewGroup", JSON.stringify(choosenGroup[0]), SuccessAddNewGroup, ErrorAddNewGroup)
} 

//***************************All Ajax Success And Error Functions**************************

// ajaxCall("PUT", "../api/group/UpdateGroup", JSON.stringify(choosenGroup[0]), UpdateNumSuccess, UpdateNumError);
//**************************************************************************************************************
function UpdateNumSuccess() {
    console.log("Group Is Successfully Updated");
}
function UpdateNumError() {
    console.log("Error update Group");
}

// ajaxCall("POST", "../api/group/insertNewGroup", JSON.stringify(choosenGroup[0]), SuccessAddNewGroup, ErrorAddNewGroup)
//**************************************************************************************************************
function SuccessAddNewGroup(data) {
    console.log("A New Group is successfully Added!");
    User.Group_Version = choosenGroup[0].Group_Version;
    User.Group_Id = choosenGroup[0].Group_Id;
    ajaxCall("POST", "../api/User", JSON.stringify(User), userAddSuccefully, userNotAdded);
}
function ErrorAddNewGroup() {
    console.log("sorry update failed");
}

//ajaxCall("POST", "../api/User", JSON.stringify(User), userAddSuccefully, userNotAdded);
//**************************************************************************************
function userAddSuccefully(data) {
    ajaxCall("PUT", "../api/group/UpdateGroup", JSON.stringify(choosenGroup[0]), UpdateNumSuccess, UpdateNumError);
    Swal.fire({
        title: 'Success',
        text: 'Added a New User',
        imageUrl: "../Images/success.png",
        imageWidth: 400,
        imageHeight: 200,
        imageAlt: 'Custom image',
        animation: false
    })
}
function userNotAdded() {
    console.log("User Not Add To DB");
}

// ajaxCall("GET", "../api/group/getAllGroup?day=" + groupDay + "&grouptime=" + groupTime+"&education=" + educationLevel, "", SuccessGetAllGroup, ErrorGetAllGroup);
//********************************************************************************************
function SuccessGetAllGroup(group) {
    choosenGroup = group;
    AddUserToGroup();
}
function ErrorGetAllGroup() {
    console.log("Error get all groups from DB");
}

