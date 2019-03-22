var classes = [];
var cApprovedOrder = [];
var cNotApproved = [];
var sectionArr = [];
var notReadyClasses;
var waitingClasses;
var readyClasses;



$(document).ready(function () {

    ajaxCall("GET", "../api/class", "", getAllClassFromDb, ErrorgetAllClassFromDb);
    CreateNewLesson();

});
/***********************************Success and Error Function Get Classes********************************/
function getAllClassFromDb(data) {
    classes = data;
    ajaxCall("GET", "../api/section", "", getAllSectionFromDb, ErrorgetAllSectionFromDb);
    ShowClassesFromDB(data);

}
function ErrorgetAllClassFromDb() {
    alert("Error get Classes");
}
/***********************************End Success and Error Function Get Classes****************************/


/***********************************Success and Error Function Get Section********************************/

function getAllSectionFromDb(AllSectionData) {
    sectionArr = AllSectionData;
    InsertSectionToClass();
    //יש לנו כרגע שני מערכים של הראשון של שיעורים והשני של מקטעים

}
function ErrorgetAllSectionFromDb() {
    alert("Error loading section from DB");
}
/***********************************End Success and Error Function Get Section*****************************/

/***********************************InsertSectionToClass Function******************************************/
//הכנסה של המקטעים לתוך המערכים של השיעורים המתאימים
function InsertSectionToClass() {

    for (var i = 0; i < classes.length; i++) {
        classes[i].Sections = new Array();
        for (var j = 0; j < sectionArr.length; j++) {
            if (sectionArr[j].ClassId == classes[i].Id) {

                classes[i].Sections.push(sectionArr[j]);
            }
        }
    }



}

/***********************************END InsertSectionToClass Function**************************************/


/*חלוקה לעמודות המתאימות לפי סטטוס השיעור*/
function ShowClassesFromDB(data) {


    CheckStatus();
    let status;
    let id;
    let title;
    var list;
    let type;

    for (var i = 0; i < cNotApproved.length; i++) {
        status = cNotApproved[i].Status;
        id = cNotApproved[i].Id;
        title = cNotApproved[i].Title;
        list = document.getElementById(status);
        type = 1;

        temp = list.innerHTML;
        temp = temp + "<li id=" + id + " class='drag-item' style='position:relative;text-align:right'> " + title + "<button onclick='getSections(" + id + "," + type + ")'>Edit sections</button></li > ";
        list.innerHTML = temp;

    }
    //Sorting the Approved classes
    cApprovedOrder = cApprovedOrder.sort((a, b) => (a.Position > b.Position) ? 1 : -1);

    for (var j = 0; j < cApprovedOrder.length; j++) {
        status = cApprovedOrder[j].Status;
        id = cApprovedOrder[j].Id;
        title = cApprovedOrder[j].Title;
        list = document.getElementById(status);
        type = 2;

        temp = list.innerHTML;
        temp = temp + "<li id=" + id + " class='drag-item' style='position:relative;text-align:right'> " + title + "<button onclick='getSections(" + id + "," + type + ")'>Edit sections</button></li > ";
        list.innerHTML = temp;
    }

}
//ממיין את השיעורים למערך של מוכנים ומערך של לא מוכנים
function CheckStatus() {
    for (var i = 0; i < classes.length; i++) {


        switch (classes[i].Status) {
            case 2:
            case 3:
                cNotApproved.push(classes[i]);
                break;
            case 4:
                cApprovedOrder.push(classes[i]);
                break;
        }
    }
}

//עדכון סטטוס לכל השיעורים לפני הכנסתם ל- DB

function SaveLesson() {


    notReadyClasses = document.getElementById("2").children;
    waitingClasses = document.getElementById("3").children;
    readyClasses = document.getElementById("4").children;

    for (var i = 0; i < notReadyClasses.length; i++) {
        for (var j = 0; j < classes.length; j++) {
            if (classes[j].Id == notReadyClasses[i].id) {
                classes[j].Status = 2;
                classes[j].Position = -1;

            }

        }

    }
    for (var i = 0; i < waitingClasses.length; i++) {
        for (var j = 0; j < classes.length; j++) {
            if (classes[j].Id == waitingClasses[i].id) {
                classes[j].Status = 3;
                classes[j].Position = -1;
            }

        }
    }
    for (var i = 0; i < readyClasses.length; i++) {
        for (var j = 0; j < classes.length; j++) {
            if (classes[j].Id == readyClasses[i].id) {
                classes[j].Status = 4;
                classes[j].Position = i;
            }

        }
    }


}

//***********************************************************************************************************************
var counter = 0;



//************************************Passing Section To Section Page****************************************************

function getSections(specific_class_id, type) {
    if (type == 1) {
        for (var i = 0; i < cNotApproved.length; i++) {
            if (cNotApproved[i].Id == specific_class_id) {
                window.localStorage.setItem("classes", JSON.stringify(cNotApproved[i])); // Saving
                window.location = 'SectionEditor.html';
            }
        }
    }

    else {
        for (var i = 0; i < cApprovedOrder.length; i++) {
            if (cApprovedOrder[i].Id == specific_class_id) {
                window.localStorage.setItem("classes", JSON.stringify(cApprovedOrder[i])); // Saving
                window.location = 'SectionEditor.html';

            }
        }
    }
}

//לעדכן את הגירסה של השיעורים לחדשה יותר
function SaveClassToDB() {
    SaveLesson();
    let oldVersion = classes[0].Version;
    for (var i = 0; i < classes.length; i++) {
        classes[i].Version = oldVersion + 1;
    }
    ajaxCall("POST", "../api/class/classArray", JSON.stringify(classes), ClassAddSuccess, CalssAddError);

}
function ClassAddSuccess(data) {
    alert("Success add Class");
}
function CalssAddError() {
    alert("Error add Class");
}

function AddLesson() {
    let newClass = JSON.parse(window.localStorage.getItem("newClass")).Sections;
    var title = newClass.Title;
    var list = document.getElementById("3");
    temp = list.innerHTML;

    temp = temp + "<li id=" + counter + " class='drag-item' > " + title + "</li > ";
    list.innerHTML = temp;
}


function Empty() {

    document.getElementById("6").innerHTML = "";
}
//**********************************************************************************************************************


function CreateNewLesson() {
 localStorage.removeItem("classes");
    
}

//function SaveClassToDB() {
//    //need to add identifier if we came from another page
//    let Class = JSON.parse(window.localStorage.getItem("newClass"));
//    alert(Class.Title);
//    //ajaxCall("POST", "../api/class", JSON.stringify(Class), ClassAddSuccess, CalssAddError);

//}


