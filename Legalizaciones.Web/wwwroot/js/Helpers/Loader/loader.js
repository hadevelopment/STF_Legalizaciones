﻿!function (i) { function s() { var s = i(window).width(), c = i(window).height(), d = i(".fl").outerWidth(), e = i(".fl").outerHeight(); i(".fl").css({ position: "absolute", left: s / 2 - d / 2, top: c / 2 - e / 2 }) } i.fn.fakeLoader = function (c) { var d = i.extend({ timeToHide: 1200, pos: "fixed", top: "0px", left: "0px", width: "100%", height: "100%", zIndex: "999", bgColor: "#2ecc71", spinner: "spinner7", imagePath: "" }, c), e = '<div class="fl spinner1"><div class="double-bounce1"></div><div class="double-bounce2"></div></div>', l = i(this), n = { position: d.pos, width: d.width, height: d.height, top: d.top, left: d.left }; return l.css(n), l.each(function () { switch (d.spinner) { case "spinner1": l.html(e); break; case "spinner2": l.html('<div class="fl spinner2"><div class="spinner-container container1"><div class="circle1"></div><div class="circle2"></div><div class="circle3"></div><div class="circle4"></div></div><div class="spinner-container container2"><div class="circle1"></div><div class="circle2"></div><div class="circle3"></div><div class="circle4"></div></div><div class="spinner-container container3"><div class="circle1"></div><div class="circle2"></div><div class="circle3"></div><div class="circle4"></div></div></div>'); break; case "spinner3": l.html('<div class="fl spinner3"><div class="dot1"></div><div class="dot2"></div></div>'); break; case "spinner4": l.html('<div class="fl spinner4"></div>'); break; case "spinner5": l.html('<div class="fl spinner5"><div class="cube1"></div><div class="cube2"></div></div>'); break; case "spinner6": l.html('<div class="fl spinner6"><div class="rect1"></div><div class="rect2"></div><div class="rect3"></div><div class="rect4"></div><div class="rect5"></div></div>'); break; case "spinner7": l.html('<div class="fl spinner7"><div class="circ1"></div><div class="circ2"></div><div class="circ3"></div><div class="circ4"></div></div>'); break; default: l.html(e) }"" != d.imagePath && l.html('<div class="fl"><img src="' + d.imagePath + '"></div>'), s() }), setTimeout(function () { i(l).fadeOut() }, d.timeToHide), this.css({ backgroundColor: d.bgColor, zIndex: d.zIndex }) }, i(window).on("load", function () { s(), i(window).resize(function () { s() }) }) }(jQuery);