//Global Var
var allGroupsArr = [];
var allUsersArr = [];
var allUserInClass = [];
var choosenGroup = [];
var educationType;
var smartPerDay = [{ "Sun": 0 }, { "Mon": 0 }, { "Tue": 0 }, { "Wed": 0 }, { "Thu": 0 }];;//Education=3
var okPerDay = [{ "Sun": 0 }, { "Mon": 0 }, { "Tue": 0 }, { "Wed": 0 }, { "Thu": 0 }];;//Education=2
var finePerDay = [{ "Sun": 0 }, { "Mon": 0 }, { "Tue": 0 }, { "Wed": 0 }, { "Thu": 0 }];//Education=1



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
//*************************************************************************************
function sumPerDay() {

    for (var i = 0; i < allGroupsArr.length; i++) {
        if (allGroupsArr[i].IsFinished == false && allGroupsArr[i].Education == 1) {
            switch (allGroupsArr[i].Day1) {
                case 1:
                    finePerDay[0]["Sun"] += allGroupsArr[i].Num_Of_Registered;
                    break;
                case 2:
                    finePerDay[1]["Mon"] += allGroupsArr[i].Num_Of_Registered;
                    break;
                case 3:
                    finePerDay[2]["Tue"] += allGroupsArr[i].Num_Of_Registered;
                    break;
                case 4:
                    finePerDay[3]["Wed"] += allGroupsArr[i].Num_Of_Registered;
                    break;
                case 5:
                    finePerDay[4]["Thu"] += allGroupsArr[i].Num_Of_Registered;
                    break;
            }

        }
        else if (allGroupsArr[i].IsFinished == false && allGroupsArr[i].Education == 2) {
            switch (allGroupsArr[i].Day1) {
                case 1:
                    okPerDay[0]["Sun"] += allGroupsArr[i].Num_Of_Registered;
                    break;
                case 2:
                    okPerDay[1]["Mon"] += allGroupsArr[i].Num_Of_Registered;
                    break;
                case 3:
                    okPerDay[2]["Tue"] += allGroupsArr[i].Num_Of_Registered;
                    break;
                case 4:
                    okPerDay[3]["Wed"] += allGroupsArr[i].Num_Of_Registered;
                    break;
                case 5:
                    okPerDay[4]["Thu"] += allGroupsArr[i].Num_Of_Registered;
                    break;
            }
        }
        else if (allGroupsArr[i].IsFinished == false && allGroupsArr[i].Education == 3) {
            switch (allGroupsArr[i].Day1) {
                case 1:
                    smartPerDay[0]["Sun"] += allGroupsArr[i].Num_Of_Registered;
                    break;
                case 2:
                    smartPerDay[1]["Mon"] += allGroupsArr[i].Num_Of_Registered;
                    break;
                case 3:
                    smartPerDay[2]["Tue"] += allGroupsArr[i].Num_Of_Registered;
                    break;
                case 4:
                    smartPerDay[3]["Wed"] += allGroupsArr[i].Num_Of_Registered;
                    break;
                case 5:
                    smartPerDay[4]["Thu"] += allGroupsArr[i].Num_Of_Registered;
                    break;
            }
        }
    }
    BarChart1();
}

//יצירת גרף פאי על פי השכלה של כל המסיימים והלא מסיימים
    function GroupsPie(finishPercentage, notFinishPercentage) {
        var chart = new CanvasJS.Chart("FinishAnalysis", {
            animationEnabled: true,
            title: {
                text: "שיעור מסיימי הקורס מכלל הנרשמים לפי השכלה"
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


//יצירת בר גרף על פי קבוצות בכל יום
function BarChart1() {

    var chart = new CanvasJS.Chart("RegisteredAnalysis", {
        animationEnabled: true,
        title: {
            text: "משתמשים רשומים לפי ימים ולפי קבוצות - לקבוצות פעילות"
        },
        axisY: {
            title: "Number Of Registered"
        },
        legend: {
            cursor: "pointer",
            itemclick: toggleDataSeries
        },
        toolTip: {
            shared: true,
            content: toolTipFormatter
        },
        data: [{
            type: "bar",
            showInLegend: true,
            name: "השכלה אקדמית",
            color: "gold",
            dataPoints: [
                { y: smartPerDay[0]["Sun"], label: "ראשון" },
                { y: smartPerDay[1]["Mon"], label: "שני" },
                { y: smartPerDay[2]["Tue"], label: "שלישי" },
                { y: smartPerDay[3]["Wed"], label: "רביעי" },
                { y: smartPerDay[4]["Thu"], label: "חמישי" }
                
            ]
        },
        {
            type: "bar",
            showInLegend: true,
            name: "השכלה תיכונית",
            color: "silver",
            dataPoints: [
                { y: okPerDay[0]["Sun"], label: "ראשון" },
                { y: okPerDay[1]["Mon"], label: "שני" },
                { y: okPerDay[2]["Tue"], label: "שלישי" },
                { y: okPerDay[3]["Wed"], label: "רביעי" },
                { y: okPerDay[4]["Thu"], label: "חמישי" }
            ]
        },
        {
            type: "bar",
            showInLegend: true,
            name: "ללא השכלה/ השכלה מינימלית",
            color: "#A57164",
            dataPoints: [
                { y: finePerDay[0]["Sun"], label: "ראשון" },
                { y: finePerDay[1]["Mon"], label: "שני" },
                { y: finePerDay[2]["Tue"], label: "שלישי" },
                { y: finePerDay[3]["Wed"], label: "רביעי" },
                { y: finePerDay[4]["Thu"], label: "חמישי" }
            ]
        }]
    });
    chart.render();

    function toolTipFormatter(e) {
        var str = "";
        var total = 0;
        var str3;
        var str2;
        for (var i = 0; i < e.entries.length; i++) {
            var str1 = "<span style= \"color:" + e.entries[i].dataSeries.color + "\">" + e.entries[i].dataSeries.name + "</span>: <strong>" + e.entries[i].dataPoint.y + "</strong> <br/>";
            total = e.entries[i].dataPoint.y + total;
            str = str.concat(str1);
        }
        str2 = "<strong>" + e.entries[0].dataPoint.label + "</strong> <br/>";
        str3 = "<span style = \"color:Tomato\">Total: </span><strong>" + total + "</strong><br/>";
        return (str2.concat(str)).concat(str3);
    }

    function toggleDataSeries(e) {
        if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
            e.dataSeries.visible = false;
        }
        else {
            e.dataSeries.visible = true;
        }
        chart.render();
    }

}   