
var tips={isIE6:!!window.ActiveXObject&&!window.XMLHttpRequest,slide:{timer:"slideTimer",show:function(e,str){clearInterval(this.timer);if($("#slideTips").length>0){$("#slideTips").hide().remove();}
var sldClass;switch(e){case 1:sldClass="background-position:0 -26px;";break;case 2:sldClass="background-position:0 -52px;";break;default:sldClass="background-position:0 0;";break;}
var tipStr="<div id=\"slideTips\"";tipStr+=" style=\"padding:0 10px;height:26px;line-height:26px;position:"+(tips.isIE6?"absolute":"fixed")+";left:50%;top:-26px;display:none;font-weight:bold;color:#fff;\ ";tipStr+="background-image:url(http://images.sneduyun.com/base/mini/HQ.minitips.slide.gif);z-index:2000;"+sldClass;tipStr+="-webkit-box-shadow:0 0 10px black;-moz-box-shadow:0 0 10px black;moz-border-radius:10px;\">";tipStr+=str+"</div>";$(tipStr).appendTo("body");var doTop;if(tips.isIE6){doTop=$(window).scrollTop();}else{doTop=0;}
$("#slideTips").show().animate({top:doTop},"slow").css({left:(($(window).width()-$("#slideTips").width())/2)+"px"});if(tips.isIE6){$(window).bind("scroll",function(){$("#slideTips").css({top:$(window).scrollTop()+"px"})});}},hide:function(){$("#slideTips").animate({top:-26},"slow",function(){$(this).remove();});},alert:function(e,str,t){var times=t||4000;this.show(e,str);this.timer=setInterval(function(){tips.slide.hide();},times);}},spring:{timer:"springTimer",show:function(e,str){clearInterval(this.timer);if($("#springTips").length>0){$("#springTips").hide().remove();}
var sprClass;switch(e){case 1:sprClass="<img src=\"http://images.sneduyun.com/base/success.gif\" width=\"16\" height=\"16\" /> ";break;case 2:sprClass="<img src=\"http://images.sneduyun.com/base/warn.gif\" width=\"16\" height=\"16\" /> ";break;default:sprClass="";break;}
var tipStr="<div id=\"springTips\" style=\"height:60px;position:"+(tips.isIE6?"absolute":"fixed")+";left:50%;top:0;display:none;z-index:2000;\">";tipStr+="<span style=\"display:inline-block;height:52px;position:relative;padding:4px;\">";tipStr+="<div id=\"springOpacity\" style=\"height:60px;position:absolute;left:0;top:0;z-index:-1;background:#000;filter:alpha(opacity=20);opacity:0.20;-moz-border-radius:5px;border-radius:5px;\"></div>";tipStr+="<span style=\"display:inline-block;height:50px;line-height:50px;border:1px #666 solid;padding:0 10px;color:#666;background-color:#fff;\">";tipStr+=sprClass+"<span style=\"display:inline-block;\"> "+str+"</span>";tipStr+="</span>";tipStr+="</span>";tipStr+="</div>";$(tipStr).appendTo("body");var doTop;if(tips.isIE6){doTop=(document.documentElement.clientHeight-$("#springTips").height())/2+$(window).scrollTop();}else{doTop=($(window).height()-$("#springTips").height())/2;}
$("#springTips").show().css({left:(($(window).width()-$("#springTips").width())/2)+"px",top:doTop+"px"});$("#springOpacity").width($("#springTips").width());if(tips.isIE6){$(window).bind("scroll",function(){$("#springTips").css({top:((document.documentElement.clientHeight-$("#springTips").height())/2+$(window).scrollTop())+"px"})});}},hide:function(){$("#springTips").hide().remove();},alert:function(e,str,t){var times=t||4000;this.show(e,str);this.timer=setInterval(function(){tips.spring.hide();},times);}},blink:{timer:"blinkTimer",show:function(e,bgColor,t){t=t||0;bgColor=bgColor||"#FFF371";var objBgColor=$("#"+e).css("background-color").toLowerCase();var bColor;if(objBgColor!=bgColor){$("#"+e).css({"background-color":""+bgColor+""});bColor=objBgColor;}else{$("#"+e).css({"background-color":""+objBgColor+""});bColor=bgColor;}
t++;this.timer=setTimeout("tips.blink.show(\""+e+"\",\""+bColor+"\","+t+")",150);if(t==8){clearTimeout(this.timer);}}}}