var leftListJSON;
var rightListJSON;

var ToLeftBtn; // кнопка переноса влево
var ToRightBtn; // кнопка переноса вправо
var AllToLeftBtn; // кнопка переноса влево всего списка
var AllToRightBtn; // // кнопка переноса вправо всего списка

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

// вывести на экран один элемент списка
function Render(someId, someArr)
{
    Clear(someId);
    someArr.forEach(function (item)
    {
        $(someId).append(`<option class = "leftOption" value='${item}'>${item}</option>`);
    });
}

// очистить с
function Clear(someId)
{
    $(someId).empty()
}

// запись на сервер
function SaveCondition(leftJson, rightJson)
{
    $.ajax({type: "POST",
            traditional: true,
            url: "/Home/SaveLists",
            data: { leftArray: leftJson, rightArray: rightJson }, });
}

// перенести элемент в другой список
function Replace(from, to, item)
{
    var index = from.indexOf(item);

    if (index >= 0)
    {
        if (item)
        {
            to.push(from[index]);
            from.splice(index, 1)
        }
    }
}

// перенести все элементы
function ReplaceAll(from, to)
{
    for (var i = 0; i < from.length; i++)
        to.push(from[i]);

    from.splice(0)
}

// клик по кнопке влево
function ToLeftBtnClick()
{
    var option = $('option:selected').text();
    Replace(RightList, LeftList, option);
    ReplaceElements();
}

// клик по кнопке вправо
function ToRightBtnClick()
{
    var option = $('option:selected').text();
    Replace(LeftList, RightList, option);
    ReplaceElements(); 
}

// // клик по кнопке все влево
function AllToLeftBtnClick()
{
    ReplaceAll(RightList, LeftList);
    ReplaceElements();
}

// // клик по кнопке все вправо
function AllToRightBtnClick()
{
    ReplaceAll(LeftList, RightList);
    ReplaceElements();
}

//вывести все списки
function RenderAll()
{
    Render("#leftList", LeftList);
    Render("#rightList", RightList);
}

// переместить
function ReplaceElements()
{
    leftListJSON = JSON.stringify(LeftList);
    rightListJSON = JSON.stringify(RightList);
    RenderAll();
    SaveCondition(leftListJSON, rightListJSON);
}

