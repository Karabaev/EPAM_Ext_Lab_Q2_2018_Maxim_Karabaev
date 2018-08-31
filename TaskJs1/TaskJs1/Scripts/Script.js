var leftArrJSON;
var rightArrJSON;

var ToLeftBtn;
var ToRightBtn;
var AllToLeftBtn;
var AllToRightBtn;

$(document).ready(function () {
    ToLeftBtn = $("#toLeftMoveBtn");
    ToRightBtn = $("#toRightMoveBtn");
    AllToLeftBtn = $("#allToLeftMoveBtn");
    AllToRightBtn = $("#allToRightMoveBtn");

    ToLeftBtn.off();
    ToRightBtn.off();
    AllToLeftBtn.off();
    AllToRightBtn.off();

    ToLeftBtn.bind('click', ToLeftBtnClick);
    ToRightBtn.bind('click', ToRightBtnClick);
    AllToLeftBtn.bind('click', AllToLeftBtnClick);
    AllToRightBtn.bind('click', AllToRightBtnClick);

    RenderAll();
});


function Render(someId, someArr)
{
    Clear(someId);
    someArr.forEach(function (item)
    {
        $(someId).append(`<option class = "leftOption" value='${item}'>${item}</option>`);
    });
}

function Clear(someId)
{
    $(someId).empty()
}

function SaveCondition(leftJson, rightJson)
{
    $.ajax({type: "POST",
            traditional: true,
            url: "/Home/SaveToLeftList",
            data: { leftArray: leftJson, rightArray: rightJson }, });
}

function Replace(from, to, item)
{
    var index = from.indexOf(item);

    if (item)
    {
        to.push(from[index]);
        from.splice(index, 1)
    }
}

function ReplaceAll(from, to)
{
    for (var i = 0; i < from.length; i++)
        to.push(from[i]);

    from.splice(0)
}

function ToLeftBtnClick()
{
    Replace(RightList, LeftList, $('option:selected').text());
    ReplaceElements();
  //  alert("Left move");
}

function ToRightBtnClick()
{
    Replace(LeftList, RightList, $('option:selected').text());
    ReplaceElements();
  //  alert($('.leftOption:selected').text());
}

function AllToLeftBtnClick()
{
    ReplaceAll(RightList, LeftList);
    ReplaceElements();
 //   alert("All left move");
}

function AllToRightBtnClick()
{
    ReplaceAll(LeftList, RightList);
    ReplaceElements();
  //  alert("All right move");
}


function RenderAll()
{
    Render("#leftList", LeftList);
    Render("#rightList", RightList);
}

function ReplaceElements()
{
    leftArrJSON = JSON.stringify(LeftList);
    rightArrJSON = JSON.stringify(RightList);
    RenderAll();
    SaveCondition(leftArrJSON, rightArrJSON);
}

