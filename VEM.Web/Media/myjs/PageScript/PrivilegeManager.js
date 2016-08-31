$(function () {
    $('#tree_1_collapse').click(function () {
        $('.tree-toggle', $('#tree_1 > li > ul')).addClass("closed");
        $('.branch', $('#tree_1 > li > ul')).removeClass("in");
    });
    $('#tree_1_expand').click(function () {
        $('.tree-toggle', $('#tree_1 > li > ul')).removeClass("closed");
        $('.branch', $('#tree_1 > li > ul')).addClass("in");
    });
   
    $('.tree-toggle').on('click', function (e) {
        var children = $(this).parent().find(' > ul > li');
        if (children.parent().hasClass("in")) {
            $(this).addClass('closed');
            children.parent().removeClass('in');
        } else {
            $(this).removeClass('closed');
            children.parent().addClass('in');
        }
        e.stopPropagation();
    });
});

function NewButtonClick(btn) {
    var checkbox = $(btn).find("input[type='checkbox']")
    if (checkbox.attr("checked")) {
        checkbox.attr("checked", false).parent().attr("class", "");
    } else {
        checkbox.attr("checked", true).parent().attr("class", "checked");
    }
}

function CheckAll(btn, classId,ajaxUrl, id) {
    var status;
    var menuidArray;
    if ($(btn).html() == "全选") {
        $(btn).html("取消全选");
        status = true;
    } else {
        $(btn).html("全选");
        status = false;
    }
    if (classId == "checkMenu") { menuidArray = new Array(); }
    $("." + classId).find("input[type='checkbox']").each(function () {
        $(this).attr("checked", true).parent().attr("class", "checked");
        if (status) {
            if (classId == "checkMenu") {
                var menuid = $(this).parent().parent().parent().attr("menuId");
                menuidArray.push(menuid);
            }
        } else {
            $(this).attr("checked", false).parent().attr("class", "");
            if (classId == "checkMenu") {
                $("#buttonDiv").html("");
            }
        }
    });
    if (classId == "checkMenu") {
        AjaxRequest.submit(ajaxUrl, { menuidArray: menuidArray, thisId: id },
                function (result) {
                    $("#buttonDiv").html(result);
                },
                function (XMLHttpRequest) {
                });
    }

}

function AddCheckButton(btn, e, ajaxUrl, id) {
    var menuid = $(btn).attr("menuCode");
    var checkbox = $(btn).find("input[type='checkbox']")
    if (checkbox.attr("checked")) {
        checkbox.attr("checked", false).parent().attr("class", "");
        $("ul[menuBtnListCode='" + menuid + "']").remove();
    } else {
        checkbox.attr("checked", true).parent().attr("class", "checked");
        AjaxRequest.submit(ajaxUrl, { menuCode: menuid, thisId: id },
            function (result) {$("#buttonDiv").append(result);},
            function (XMLHttpRequest) {});
    }
    e.stopPropagation();
}