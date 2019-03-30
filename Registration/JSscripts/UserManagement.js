//Global Var
var allGroupsArr = [];
var allUsersArr = [];
var allUserInClass = [];
var choosenGroup = [];
var educationType;


//Retrive all groups from DB when document is ready
//*************************************************
$(document).ready(function () {
    ajaxCall("GET", "../api/group/getAllGroups", "", GetAllGroupsSuccess, ErrorGetAllGroups);

});

//Ajax Call function Error and Success
//*************************************************
function GetAllGroupsSuccess(groupsFromDb)
{
    allGroupsArr = groupsFromDb;
   
    ajaxCall("GET", "../api/user/getAllUser", "", GetAllUsersSuccess, ErrorGetAllUsers);
}
function ErrorGetAllGroups() {
    alert("Error get all groups from DB");
}
function GetAllUsersSuccess(allUsers) {
    allUsersArr = allUsers;
    ajaxCall("GET", "../api/userinclass/getAllUserInClass", "", GetAllUsersInClassSuccess, ErrorGetAllUserInClass);
}
function ErrorGetAllUsers() {
    alert("Error get all Users from DB");
}
function GetAllUsersInClassSuccess(usersInClass) {
    allUserInClass = usersInClass;
    
}
function ErrorGetAllUserInClass() {
    alert("Error get all Users In Class from DB");
}

//כאשר לוחצים כל הכפתור 'לחץ להצגת גרף' הפונקציה הזאת תרוץ
//************************************************************
function devideToGroups() {
    educationType = document.getElementById("educationType").value//תפיסת סוג ההשכלה
    for (var i = 0; i < allGroupsArr.length; i++) {
        if (allGroupsArr[i].Education == educationType) {
            choosenGroup.push(allGroupsArr[i]);
           
        }
    }
    let groupFinish = 0;
    let groupNotFinish = 0;
    for (var i = 0; i < choosenGroup.length; i++) {
        if (choosenGroup[i].IsFinished == true)
            groupFinish += 1;
        else
            groupNotFinish += 1;
    }
    let finishPercentage = (groupFinish / choosenGroup.length) * 100;
    let notFinishPercentage = (groupNotFinish / choosenGroup.length) * 100;
    GroupsPie(finishPercentage, notFinishPercentage)
}

{
    function GroupsPie(finishPercentage, notFinishPercentage) {
        var chart = new CanvasJS.Chart("chartContainer", {
            animationEnabled: true,
            title: {
                text: "Admin Dashborad"
            },
            data: [{
                type: "pie",
                startAngle: 240,
                yValueFormatString: "##0.00\"%\"",
                indexLabel: "{label} {y}",
                dataPoints: [
                    { y: finishPercentage, label: "Finished" },
                    { y: notFinishPercentage, label: "No Finished" }


                ]
            }]
        });
        chart.render();
    }



}
    