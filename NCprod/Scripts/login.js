$(function(){
	
	$('#switch_qlogin').click(function(){
		$('#switch_login').removeClass("switch_btn_focus").addClass('switch_btn');
		$('#switch_qlogin').removeClass("switch_btn").addClass('switch_btn_focus');
		$('#switch_bottom').animate({left:'0px',width:'70px'});
		$('#qlogin').css('display','none');
		$('#web_qr_login').css('display','block');
		
		});
	$('#switch_login').click(function(){
		
		$('#switch_login').removeClass("switch_btn").addClass('switch_btn_focus');
		$('#switch_qlogin').removeClass("switch_btn_focus").addClass('switch_btn');
		$('#switch_bottom').animate({left:'154px',width:'70px'});
		
		$('#qlogin').css('display','block');
		$('#web_qr_login').css('display','none');
		});
if(getParam("a")=='0')
{
	$('#switch_login').trigger('click');
}

	});
	
function logintab(){
	scrollTo(0);
	$('#switch_qlogin').removeClass("switch_btn_focus").addClass('switch_btn');
	$('#switch_login').removeClass("switch_btn").addClass('switch_btn_focus');
	$('#switch_bottom').animate({left:'154px',width:'96px'});
	$('#qlogin').css('display','none');
	$('#web_qr_login').css('display','block');
	
}


//根据参数名获得该参数 pname等于想要的参数名 
function getParam(pname) { 
    var params = location.search.substr(1); // 获取参数 平且去掉？ 
    var ArrParam = params.split('&'); 
    if (ArrParam.length == 1) { 
        //只有一个参数的情况 
        return params.split('=')[1]; 
    } 
    else { 
         //多个参数参数的情况 
        for (var i = 0; i < ArrParam.length; i++) { 
            if (ArrParam[i].split('=')[0] == pname) { 
                return ArrParam[i].split('=')[1]; 
            } 
        } 
    } 
}  


var reMethod = "GET",
	pwdmin = 6;

$(document).ready(function() {


    //$('#btnSave').click(function () {

	//	if ($('#user').val() == "") {
	//		$('#user').focus().css({
	//			border: "1px solid red",
	//			boxShadow: "0 0 2px red"
	//		});
	//		alert("用户名不能为空!");
	//		//$('#userCue').html("<font color='red'><b>×用户名不能为空</b></font>");
	//		return;
	//	}
	//	if ($('#user').val().length < 4 || $('#user').val().length > 16) {

	//		$('#user').focus().css({
	//			border: "1px solid red",
	//			boxShadow: "0 0 2px red"
	//		});
	//		alert("用户名位4-16字符");
	//		//$('#userCue').html("<font color='red'><b>×用户名位4-16字符</b></font>");
	//		return;

	//	}
	//	if ($('#passwd').val().length < pwdmin) {
	//	    $('#passwd').focus();
	//	    alert("密码不能小于"+ pwdmin +"位");
	//		//$('#userCue').html("<font color='red'><b>×密码不能小于" + pwdmin + "位</b></font>");
	//		return;
	//	}
	//	if ($('#passwd2').val() != $('#passwd').val()) {
	//	    $('#passwd2').focus();
	//	    alert("两次密码不一致！");
	//		//$('#userCue').html("<font color='red'><b>×两次密码不一致！</b></font>");
	//		return;
	//	}

	//	//var sqq = /^[1-9]{1}[0-9]{4,9}$/;
	//	if ($('#phone').val()=="" || $('#phone').val().length != 11) {
	//	    $('#phone').focus().css({
	//			border: "1px solid red",
	//			boxShadow: "0 0 2px red"
	//	    });
	//	    alert("手机号码输入格式不正确");
	//	    return;
	//		//$('#userCue').html("<font color='red'><b>×QQ号码格式不正确</b></font>");return false;
	//	}
	//	//else {
	//	//	$('#phone').css({
	//	//		border: "1px solid #D7D7D7",
	//	//		boxShadow: "none"
	//	//	});
			
	//	//}

	//	//$.ajax({
	//	//    type: reMethod,
	//	//    //url: "/member/ajaxyz.php",
	//	//    data: "uid=" + $("#user").val() + '&temp=' + new Date(),
	//	//    dataType: 'html',
	//	//    success: function (result) {

	//	//        if (result.length > 2) {
	//	//            $('#user').focus().css({
	//	//                border: "1px solid red",
	//	//                boxShadow: "0 0 2px red"
	//	//            }); $("#userCue").html(result);
	//	//            return false;
	//	//        } else {
	//	//            $('#user').css({
	//	//                border: "1px solid #D7D7D7",
	//	//                boxShadow: "none"
	//	//            });
	//	//        }

	//	//    }
    //    //});
        
	//	var article = GetArticle();
	//	$.ajax({
	//	    type: "POST",
	//	    contentType: "application/json;utf-8",
	//	    url: "/Home/Save",
	//	    data: "{json:'" + $.toJSON(article) + "'}",
	//	    dataType: "",
	//	    success: function (data, textStatus) {
	//	        if (data === "existed") {
	//	            alert("该用户已注册！");
	//	            $('#user').val('');
	//	            $('#phone').val('');
	//	            $('#passwd').val('');
	//	            $('#passwd2').val('');
	//	        }
	//	        else if (data == "success") {
	//	            alert("注册成功！");
	//	            $('#user').val('');
	//	            $('#phone').val('');
	//	            $('#passwd').val('');
	//	            $('#passwd2').val('');
	//	        }
	//	        else {
	//	            alert(data);
	//	        }
	//	    },
	//	    complete: function (XMLHttpRequest, textStatus) {
	//	        //HideLoading();
	//	    },
	//	    error: function (statusText, responseText) {
	//	        //请求出错处理
	//	        alert(statusText.responseText);
	//	    }
	//	});

	    
	//    //手机号码验证信息
	//	//function isMobil(s) {
	//	//    var patrn = /(^0{0,1}1[3|4|5|6|7|8|9][0-9]{9}$)/;
	//	//    if (!patrn.exec(s)) {
	//	//        return false;
	//	//    }
	//	//    return true;
	//	//}
	//    //校验手机号
	//	//function checkPhone() {
	//	//    var result = "fail";
	//	//    var Mobile = $("#phone").val();
	//	//    if (Mobile != "") {
	//	//        if (isMobil(Mobile)) {
	//	//            //$("#divphoneerror").css('display', 'none');
	//	//            result = "success";
	//	//        }
	//	//        else {
	//	//            alert("输入手机号码错误！");
	//	//            //$("#divphoneerror").css('display', 'block');
	//	//            result = "fail";
	//	//        }
	//	//    }
	//	//    return result;
	//	//}
	//	$('#regUser').submit();
    //});
    // 获取所有信息
    //function GetArticle() {

    //    //获取radio的值
    //    var rds = document.getElementsByName("iCheck");
    //    var rdVal;
    //    for (var i = 0; i < rds.length; i++) {
    //        if (rds.item(i).checked) {
    //            rdVal = rds.item(i).getAttribute("value");
    //            break;
    //        }
    //        else {
    //            continue;
    //        }
    //    }

    //    var article = new Object();
    //    //还有成员类型
    //    article.roleid = rdVal;
    //    article.user = $("#user").val();
    //    article.phone = $("#phone").val();
    //    article.passwd = $("#passwd").val();
    //    return article;
    //}
	

});