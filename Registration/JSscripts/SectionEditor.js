
dragula([
    document.getElementById('1'),
    document.getElementById('2'),
    document.getElementById('3'),
    document.getElementById('4'),
    document.getElementById('5'),
    document.getElementById('6')]).


    on('drag', function (el) {

        // add 'is-moving' class to element being dragged
        el.classList.add('is-moving');
    }).
    on('dragend', function (el) {

        // remove 'is-moving' class from element after dragging has stopped
        el.classList.remove('is-moving');

        // add the 'is-moved' class for 600ms then remove it
        window.setTimeout(function () {
            el.classList.add('is-moved');
            window.setTimeout(function () {
                el.classList.remove('is-moved');
            }, 600);
        }, 100);
    });
//***********************************************************************************************************************
var NewSectionId = 305;
var section_status = 2;
var readySectionsArr = [];
var generalSectionsArr = [];
var sNotApproved = [];
var sApprovedOrder = [];
var classMaxId;
var classMaxVersion;
var class_title;
var class_desc;
var class_position;
var class_status;
var class_id;
var allClasses;
var section_for_edit;
var notReady;
var waiting;
var ready;

//Template for Section
function Section_Json(id, title, content, status, position, class_version, class_id, file_path) {
    this.Id = id,
        this.Title = title,
        this.Description = content,
        this.Status = status,
        this.Position = position,
        this.Version = class_version,
        this.ClassId = class_id,
        this.FilePath = file_path
}
//הוספת אובייקט ג'ייסון של מקטע חדש לתוך מערך המקטעים הכללי
function AddSection() {
    InsertImage();
    FindMaxSectionId();
    section_status = 2;
    var title = document.getElementById("section-title").value;
    var content = document.getElementById("section-content").value;
    var file_path = document.getElementById("section-image").value;
    var ready = document.getElementById("ready").checked;

    if (ready === true) {
        section_status = 3;
    }
    //Saving the section details in JSON object
    Section_Json.Id = NewSectionId;
    Section_Json.Description = content;
    Section_Json.Title = title;
    Section_Json.FilePath = file_path;
    Section_Json.Status = section_status;
    Section_Json.Position = -1;
    Section_Json.Version = classMaxVersion;
    Section_Json.ClassId = class_id;
    var sec = new Section_Json(Section_Json.Id, Section_Json.Title, Section_Json.Description, Section_Json.Status, Section_Json.Position, Section_Json.Version, Section_Json.ClassId, Section_Json.FilePath);

    var list = document.getElementById(section_status);
    temp = list.innerHTML;
    temp = temp + "<li id=" + NewSectionId + " class='drag-item' style='position:relative;text-align:right'> " + title + "<img src='../Images/trash.png' onclick='Delete(this,id)' style='width:20px;height:20px;margin:5px;position:absolute;top:2px;left:1px' /><button id='editBTN' data-toggle='modal' data-target='#squarespaceModal'  class='button-secondary pure-button' onclick='EditThisSection(" + NewSectionId + ")'>Edit sections</button></li > ";
    list.innerHTML = temp;
    generalSectionsArr.push(sec);
    
   $("#squarespaceModal").modal("hide");
    //document.getElementById('squarespaceModal').hidden = true;

}

//**************************************
//*******Delete specific section********

function Delete(e,id) {

    if (confirm("האם ברצונך למחוק את המקטע?")) {
        let indexToRemove;
        for (var i = 0; i < generalSectionsArr.length; i++) {
            if (id == generalSectionsArr[i].Id) {
                indexToRemove = i;
                break;
            }
        }
        if (indexToRemove > -1) {
            generalSectionsArr.splice(indexToRemove, 1);
        }
        e.parentNode.parentNode.removeChild(e.parentNode);
    }     
}

/***************************End Get Last Id And Save Class*****************************************************/

function AddClass(title,description,position,status) {

    
    Class = {
        Id: class_id,
        Description: description,
        Title: title,
        Status: status,
        Position: position,
        Score: 50,
        Version: classMaxVersion,
        Sections: generalSectionsArr,
  
    }

    for (var i = 0; i < allClasses.length; i++) {

        if (allClasses[i].Id == Class.Id) {
            allClasses[i] = Class;      
        }
        else {
            for (var j = 0; j < allClasses[i].Sections.length; j++) {
                
                generalSectionsArr.push(allClasses[i].Sections[j]);             
            }
        }
        allClasses[i].Version += 1;
    }
    for (var k = 0; k < generalSectionsArr.length; k++) {
        generalSectionsArr[k].Version = classMaxVersion + 1;
    }
    ajaxCall("POST", "../api/class/classArray", JSON.stringify(allClasses), ClassAddSuccess, CalssAddError);
}


//**********************************************************************************************************************
///Sorting and Show********************************************************************************************************'

function ShowSectionsFromDB() {
    allClasses = JSON.parse(window.localStorage.getItem("allClasses"));//כל השיעורים בגרסה האחרונה
    class_object = JSON.parse(window.localStorage.getItem("classes"));//האובייקט שעושים עליו את השינוי

    generalSectionsArr = class_object.Sections;
    class_title = class_object.Title;
    class_desc = class_object.Description;
    class_position = class_object.Position;
    class_status = class_object.Status;
    class_id = class_object.Id;

    document.getElementById("class-title").value = class_title;
    document.getElementById("class-desc").value = class_desc;

    CheckStatus();
    let status;
    let id;
    let title;
    var list;

    for (var i = 0; i < sNotApproved.length; i++) {
        status = sNotApproved[i].Status;
        id = sNotApproved[i].Id;
        title = sNotApproved[i].Title;
        list = document.getElementById(status);


        temp = list.innerHTML;
        temp = temp + "<li id=" + id + " class='drag-item' style='position:relative;text-align:right'> " + title + "<img src='../Images/trash.png' onclick='Delete(this,"+id+")' style='width:20px;height:20px;margin:5px;position:absolute;top:2px;left:1px' /><button id='editBTN' data-toggle='modal' data-target='#squarespaceModal'  class='button-secondary pure-button' onclick='EditThisSection(" + id + ")'>Edit sections</button></li > ";
        
        list.innerHTML = temp;

    }
    //Sorting the Approved classes
    sApprovedOrder = sApprovedOrder.sort((a, b) => (a.Position > b.Position) ? 1 : -1);

    for (var j = 0; j < sApprovedOrder.length; j++) {
        status = sApprovedOrder[j].Status;
        id = sApprovedOrder[j].Id;
        title = sApprovedOrder[j].Title;
        list = document.getElementById(status);
        temp = list.innerHTML;
        temp = temp + "<li id=" + id + " class='drag-item' style='position:relative;text-align:right'> " + title + "<img src='../Images/trash.png' onclick='Delete(this," + id +")' style='width:20px;height:20px;margin:5px;position:absolute;top:2px;left:1px' /><button id='editBTN' data-toggle='modal' data-target='#squarespaceModal' class='button-secondary pure-button' onclick='EditThisSection(" + id + ")'>Edit sections</button></li > ";
       
        list.innerHTML = temp;
    }  
}
//עדכון סטטוס ומיקום למקטעים חדשים
function UpdateSectionStatus() {
  
     class_title = document.getElementById("class-title").value;
     class_desc = document.getElementById("class-desc").value;
    notReady = document.getElementById("2").children;
    waiting = document.getElementById("3").children;
    ready = document.getElementById("4").children;

    for (var i = 0; i < notReady.length; i++) {
        for (var j = 0; j < generalSectionsArr.length; j++) {
            if (generalSectionsArr[j].Id == notReady[i].id) {
                generalSectionsArr[j].Status = 2;
                generalSectionsArr[j].Position = -1;
            }
        }
    }
    for (var i = 0; i < waiting.length; i++) {
        for (var j = 0; j < generalSectionsArr.length; j++) {
            if (generalSectionsArr[j].Id == waiting[i].id) {
                generalSectionsArr[j].Status = 3;
                generalSectionsArr[j].Position = -1;
            }
        }
    }
    for (var i = 0; i < ready.length; i++) {
        for (var j = 0; j < generalSectionsArr.length; j++) {
            if (generalSectionsArr[j].Id == ready[i].id) {
                generalSectionsArr[j].Status = 4;
                generalSectionsArr[j].Position = i;
            }
        }
    }
    AddClass(class_title, class_desc, class_position, class_status);
}
//בודק שקיימים סקשנים בשיעור החדש
function CheckExistingSections() {

    let notReady2 = document.getElementById("2").children;
    let waiting2 = document.getElementById("3").children;
    let ready2 = document.getElementById("4").children;
    if (notReady2.length == 0 && waiting2.length == 0 && ready2.length == 0) {
        alert("You have to add Section to class");
        return;
    }
    UpdateSectionStatus();
}

//Check current sections' statuses-(not ready/ready but need approval/approved sections)
function CheckStatus() {
    for (var i = 0; i < generalSectionsArr.length; i++) {


        switch (generalSectionsArr[i].Status) {
            case 2:
            case 3:
                sNotApproved.push(generalSectionsArr[i]);
                break;
            case 4:
                sApprovedOrder.push(generalSectionsArr[i]);
                break;
        }
    }
}

//Show specific section on the modal for updating
function EditThisSection(section_id) {
    for (var i = 0; i < generalSectionsArr.length; i++) {
        if (generalSectionsArr[i].Id == section_id) {
            section_for_edit = generalSectionsArr[i];
            break;
        }
    }
    document.getElementById("section-title").value = section_for_edit.Title;
    document.getElementById("section-content").value = section_for_edit.Description;
    document.getElementById("pathName").innerHTML = section_for_edit.FilePath;
    document.getElementById("readyDiv").style.display = "none";
    lastIndex = section_for_edit.FilePath.lastIndexOf("\\") + 1;
    fileName = section_for_edit.FilePath.substring(lastIndex);
    document.getElementById("fileImg").src = "../Files/" + fileName;

   document.getElementById("SaveBtn").innerHTML = "<button onclick='UpdateSection("+section_for_edit.Id+")' type='button' id='saveImage' class='btn btn-default btn-hover-green' data-action='save' role='button'>שמור</button>";
}

//Update section details
function UpdateSection(id) {
    InsertImage();
    document.getElementById("SaveBtn").innerHTML = "<button onclick='AddSection()' type='button' id='saveImage' class='btn btn-default btn-hover-green' data-action='save' role='button'>שמור</button>";

   
    var updated_title = document.getElementById("section-title").value;
    var updated_content = document.getElementById("section-content").value;
    var updated_file_path = document.getElementById("pathName").innerHTML;
    document.getElementById(section_for_edit.Id).innerHTML = "<li id=" + id + " class='drag-item' style='position:relative;text-align:right'> " + updated_title + "<img src='../Images/trash.png' onclick='Delete(this," + id +")' style='width:20px;height:20px;margin:5px;position:absolute;top:2px;left:1px' /><button id='editBTN' data-toggle='modal' data-target='#squarespaceModal' class='button-secondary pure-button' onclick='EditThisSection(" + id + ")'>Edit sections</button></li > ";

    for (var i = 0; i < generalSectionsArr.length; i++) {
        if (generalSectionsArr[i].Id == id) {
            generalSectionsArr[i].Title = updated_title;
            generalSectionsArr[i].Description = updated_content;
            generalSectionsArr[i].FilePath = updated_file_path;
            break;
        }
    }
    document.getElementById("section-title").value = "";
    var updated_content = document.getElementById("section-content").value = "";
    var updated_file_path = document.getElementById("pathName").innerHTML = "";
}

//Display onchage img
function setImg() {
    let pic = document.getElementById("section-image").value;
    picIndex = pic.lastIndexOf("\\") + 1;
    pic = pic.substring(picIndex);
    document.getElementById("fileImg").src = "C:/Users/gavriel.zarka/Desktop/Picturesfor/" + pic;
}

$("#section-image").on("input", function () {    
    document.getElementById("pathName").innerHTML = this.value;
});

//Find currnet max Id for sections
function FindMaxSectionId() {
    if (generalSectionsArr.length > 0) {
        NewSectionId = generalSectionsArr[0].Id;
        for (var i = 1; i < generalSectionsArr.length; i++) {
            if (generalSectionsArr[i].Id > NewSectionId) {
                NewSectionId = generalSectionsArr[i].Id;
            }
        }

        NewSectionId++;
    }
    else NewSectionId = 305;
}

//******************************************All Ajax Success And Error Functions***********************************

// ajaxCall("GET", "../api/class/getid", "", GetClassIDSuccess, GetCalssIDError);
//****************************************************************************
function GetClassIDSuccess(classId) {
    //   classMaxId = classId.Id;
    classMaxVersion = classId.Version;
}
function GetCalssIDError() {
    console.log("Error Get Id");
}

//ajaxCall("POST", "../api/section/addnewsection", JSON.stringify(generalSectionsArr), SuccessfullyAddNewSections, ErrorAddNewSections);
//****************************************************************************************************************
function SuccessfullyAddNewSections(data) {
    console.log("Successfully Add A new Sections");
    window.location.href = 'ContentReview.html';
}
function ErrorAddNewSections() {
    console.log("Error Add A new Sections");
}

//ajaxCall("POST", "../api/class/classArray", JSON.stringify(allClasses), ClassAddSuccess, CalssAddError);
//******************************************************************************************************
function ClassAddSuccess(data) {
    console.log("Success add Class");
    ajaxCall("POST", "../api/section/addnewsection", JSON.stringify(generalSectionsArr), SuccessfullyAddNewSections, ErrorAddNewSections);
}
function CalssAddError() {
    console.log("Error add Class");
}
