var maxsize = 10 * 1024 * 1024;//2M
var errMsg = "上传的附件文件不能超过2M！！！";
var tipMsg = "您的浏览器暂不支持计算上传文件的大小，确保上传文件不要超过10M，建议使用IE、FireFox、Chrome浏览器。";
var browserCfg = {};
var ua = window.navigator.userAgent;
if (ua.indexOf("MSIE") >= 1) {
    browserCfg.ie = true;
} else if (ua.indexOf("Firefox") >= 1) {
    browserCfg.firefox = true;
} else if (ua.indexOf("Chrome") >= 1) {
    browserCfg.chrome = true;
}
function checkfile(obj,id) {
    try {
        var obj_file = obj;
        if (obj_file.value == "") {
            alert("请先选择上传文件");
            return;
        }
        var filesize = 0;
        if (browserCfg.firefox || browserCfg.chrome) {
            filesize = obj_file.files[0].size;
        } else if (browserCfg.ie) {
            var obj_img = document.getElementById('tempimg');
            obj_img.dynsrc = obj_file.value;
            filesize = obj_img.fileSize;
        } else {
            alert(tipMsg);
            return;
        }
        if (filesize == -1) {
            alert(tipMsg);
            return;
        } else if (filesize > maxsize) {
            var file = $("#upfile_"+id);
            file.after(file.clone().val(""));
            file.remove();
            alert(errMsg);
        
        } else {  
            return;
        }
    } catch (e) {
        alert(e);
    }
}

function addFile() {
    var c = ($('#FileList').children().length + 1) / 2;
    if (c < 5) {
        var filebutton = '<br><input type="file" name="File" id="upfile_' + c + '"  onchange="checkfile(this,' + c + ');"  />';
        document.getElementById('FileList').insertAdjacentHTML("beforeEnd", filebutton);
    }
    else{
        alert("只能上传五个文件");

    }
}

function findSize(field_id) {
    var fileInput = $("#" + field_id)[0];
    byteSize = fileInput.files[0].fileSize;
    return (Math.ceil(byteSize / 1024)); // Size returned in KB.
}



var ajaxcount = 0;
var timehandle;
function timedCount() {
    if (ajaxcount < 6) {
        var msg = $("#btnAjaxSubmit").val() + ".";
        $("#btnAjaxSubmit").val(msg);
    }
    else {
        ajaxcount = 0;
        $("#btnAjaxSubmit").val("正在发送邮件.");
    }
    ajaxcount = ajaxcount + 1;
    getissendlist();
    timehandle = setTimeout("timedCount()", 1000);
}

function stopCount() {
    cajaxcount = 0;
    setTimeout($("#btnAjaxSubmit").val("发送"), 0);
    clearTimeout(timehandle);
    getissendlist();
}

//读取已发送列表
function getissendlist() {
    $.ajax({
        type: 'POST',
        url: "/AsyncMail/GetIsSendList",
        data: { mailid: $("#guid").val() },
        success: function (result) {
            $("#TextAreamsage").append(result);
            if (result !== null || result !== undefined || result !== '') {
            var arr = result.split(",");
            arr.forEach(function (e) {
                if (e!== ''){
                    $("[value=" + e + "]").parent().removeClass("ischeckid");
                    $("[value=" + e + "]").parent().addClass("sendok");
                }
            })
            } 
        }
    });

    //$.post("/AsyncMail/Sendmail", { suggest: txt }, function (result) {
    //    $("span").html(result);
    //});
}


$(document).ready(function () {
    var options = {
        success: function (data) {            
            stopCount();
            $("#btnAjaxSubmit").val("发送");
            $("#btnAjaxSubmit").removeAttr("disabled");
            alert(data);
        }
    };

    // ajaxForm
    //$("#form1").ajaxForm(options);

    // ajaxSubmit
    $("#btnAjaxSubmit").click(function () {
        $("#form1").ajaxSubmit(options);
        $(this).val("正在发送邮件.");
        timedCount();
        $(this).attr("disabled", "disabled");
    });

    // 倘若btnAjaxSubmit为type="submit"的按钮或者是在表单的submit事件
    // 中调用$("#form1").ajaxSubmit(options)，那么必须调用return false
    // 防止表单的同步提交事件发生，造成重复提交。
    // eg：
    //$("#form1").submit(function () {
    //    $(this).ajaxSubmit(options);
    //    return false;
    //});

    $(".mblist").change(function () {
 
        $(this).parent().removeClass("sendok");
        if($(this).attr("checked"))
        {
            $(this).parent().addClass("ischeckid");            
        }
        else {         
            $(this).parent().removeClass("ischeckid");
        }      
    });

    //全选
    $("#selectAll").click(function () {
        $(".mblist").parent().removeClass("sendok");
        if ($("#selectAll").attr("checked")) {
            $(".mblist").attr("checked", true);
            $(".mblist").parent().addClass("ischeckid");          
        }
        else {           
            $(".mblist").attr("checked", false);
            $(".mblist").parent().removeClass("ischeckid");
        }
    });
});