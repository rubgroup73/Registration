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
var groupDay = User.PrefDay1;


//Checking User Prefhour and convert to groupHour
function CheckHour() {
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



//function FindGroup() {
//    for (var i = 0; i < allGroupsArr.length; i++) {
//        if (allGroupsArr[i].Day1 == groupDay) {
//            if (allGroupsArr[i].Hour1 == groupTime) {
//                if (allGroupsArr[i].Education == educationLevel) {
//                    if (allGroupsArr[i].Max_Partcipants < allGroupsArr[i].Num_Of_Registered) {
//                        User.Group_Id = allGroupsArr[i].Group_Id;
//                        ajaxCall("POST", "../api/User", JSON.stringify(User), userAddSuccefully, userNotAdded);
//                    }
//                    else {
//                        OpenNewGroup();
//                    }
//                }
//            }
//        }
//    }
//}

function userAddSuccefully(data) {
    swal("Added!", "User is successfully Added!", "success");
}
function userNotAdded(err) {
    alert("Sorryyyyy");
}
function SuccessGetAllGroup(allGroups) {
    allGroupsArr = allGroups;
    
}
function ErrorGetAllGroup() {
    alert("Error get all groups from DB");
}
