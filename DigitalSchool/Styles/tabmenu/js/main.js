function removeClassRegEx(a, b) {
    var c = a,
        d = c.attr("class").split(" "),
        e = b;
    for (x in d) d[x].match(e) && c.removeClass(d[x])
}
$(document).ready(function() {
    var a = "animation-slideUp";
    $("#temporary-container").load("ajax/tab-design-2.html", function() {
        $(".tab-marker > li:first-child").css("z-index", "11")
    }), $("body").on("click", ".tab-marker li", function() {
        var b = $(this),
            c = b.children("input").attr("id"),
            d = b.parents(".tab-menu").siblings("li").find("li." + c);
        b.css("z-index", "11").siblings("li").removeAttr("style"), d.siblings("li").hide(), d.show();
        var e = b.parents(".tab-menu").siblings("li.tab-content-container").find(".tab-content > div");
        removeClassRegEx(e, /^animation-/), e.addClass(a), b.parents(".tab-menu").siblings("li.tab-content-container").find(".chart-bars li span").addClass("animation-scaleX"), b.parents(".tab-menu").siblings("li.tab-content-container").find(".chart-columns li span").addClass("animation-scaleY"), b.parents(".tab-menu").siblings("li.tab-content-container").find(".chart-bubbles li span").addClass("animation-scale")
    }), $("body").on("click", ".close-x", function() {
        $(this).parents(".tab-content").fadeOut(400)
    }), $("body").on("click", ".alert-btn", function() {
        $(this).parent().fadeOut(500)
    }), $(window).resize(function() {
        $(window).width() <= 894 && $("#temporary-container").removeAttr("style")
    }), $("body").on("touchstart", ".image-container, .tooltip-marker", function() {}), $("body").on("click", ".tab-design-15 .tab-marker > li", function() {
        var a = $(this),
            b = a.parents(".tabs");
        b.find(".tab-menu .tab-marker > li").removeAttr("style"), a.hasClass("top-left-corner") ? a.css("border-top-color", "black") : a.hasClass("top-right-corner") ? a.css("border-right-color", "black") : a.hasClass("bottom-left-corner") ? a.css("border-left-color", "black") : a.css("border-bottom-color", "black")
    }), $("#tab-design-changer").on("change", function() {
        var a = $(this),
            b = ["tab-design-5", "tab-design-5-bottom", "tab-design-6", "tab-design-6-bottom", "tab-design-7", "tab-design-7-bottom", "tab-design-8", "tab-design-8-bottom"],
            c = ["tab-design-1-left", "tab-design-1-right", "tab-design-9", "tab-design-14", "tab-design-14-2", "tab-design-14-3", "tab-design-15"],
            d = ["tab-design-1-bottom", "tab-design-2-bottom", "tab-design-3-bottom", "tab-design-4-bottom", "tab-design-5-bottom", "tab-design-6-bottom", "tab-design-7-bottom", "tab-design-8-bottom", "tab-design-9", "tab-design-9-bottom", "tab-design-9-left", "tab-design-9-right", "tab-design-10-left", "tab-design-12", "tab-design-14", "tab-design-14-2", "tab-design-14-3", "tab-design-15"],
            e = $("#tab-design-changer").val();
        $("#temporary-container").load("ajax/" + e + ".html", function() {
            $(".tab-marker > li:first-child").css("z-index", "11");
            for (var f in c) {
                if ("tab-design-12" == e || "tab-design-14" == e || "tab-design-14-2" == e || "tab-design-14-3" == e || "tab-design-15" == e) {
                    a.parents(".settings-container").find("select").not("#tab-design-changer, #tab-spacing-changer, #tab-theme-changer, #tab-animation-changer").prop("disabled", !0).parent("div").css("color", "#CFCFCF");
                    break
                }
                if (e == c[f]) {
                    a.parent("div").nextUntil("#tab-themes").children("select").prop("disabled", !0).parent("div").css("color", "#CFCFCF");
                    break
                }
                a.parents("div").find("select").prop("disabled", !1).parent("div").removeAttr("style");
                for (var g in b) {
                    if (e == b[g]) {
                        a.parent("div").siblings("#tab-rounding").hide();
                        break
                    }
                    a.parent("div").siblings("#tab-rounding").show()
                }
                for (var h in d) {
                    if (e == d[h]) {
                        a.parents(".settings-container").find("#tab-arrow-changer").prop("disabled", !0).parent("div").css("color", "#CFCFCF");
                        break
                    }
                    a.parents("div").find("select").prop("disabled", !1).parent("div").removeAttr("style")
                }
            }
            a.parents(".settings-container").find("select").not("select#tab-design-changer").prop("selectedIndex", 0), a.closest("div").siblings("div").children("#tab-tight-options").prop("disabled", !0).parent("div").css("color", "#CFCFCF"), $("link#responsiveness").attr("href", "css/media-stacked.css")
        })
    }), $("#tab-spacing-changer").on("change", function() {
        var a = $(this),
            b = $(".tab-marker"),
            c = $("#tab-spacing-changer").val();
        b.removeClass("tab-marker-tight"), b.addClass(c), "tab-marker-tight" == c ? a.parent().next("div").css("color", "black").find("select").prop("disabled", !1) : a.parent().next("div").css("color", "gray").find("select").prop("disabled", !0)
    }), $("#tab-tight-options").on("change", function() {
        var a = $(".tab-marker"),
            b = $("#tab-tight-options").val();
        removeClassRegEx(a, /^tab-marker-tight-/), a.addClass(b), "tab-marker-overlap" == b && a.removeClass("tab-marker-tight-transparent tab-marker-tight-separator").addClass("tab-marker-tight"), "tab-marker-tight-separator" == b && a.removeClass("tab-marker-tight-transparent").addClass("tab-marker-tight-tight tab-marker-tight-separator"), "tab-marker-tight-transparent" == b && a.removeClass("tab-marker-tight-transparent tab-marker-tight-separator").addClass("tab-marker-tight-transparent tab-marker-tight")
    }), $("#tab-rounding-changer").on("change", function() {
        var a = $(this),
            b = a.val();
        $(".tab-marker").removeClass("tab-marker-rounded-top"), $(".tab-marker").addClass(b)
    }), $("#tab-position-changer").on("change", function() {
        var a = $(this),
            b = $(".tab-marker"),
            c = a.val();
        removeClassRegEx(b, /^tab-marker-pos/), b.addClass(c)
    }), $("#tab-theme-changer").on("change", function() {
        var a = $(this),
            b = "css/",
            c = a.val() + ".css";
        $("link#themes").attr("href", b + c)
    }), $("#tab-animation-changer").on("change", function() {
        var b = $(this),
            c = $(".tab-content > div");
        a = b.val(), removeClassRegEx(c, /^animation-/), c.addClass(a)
    }), $("#tab-arrow-changer").on("change", function() {
        var a = $(this),
            b = $(".tab-marker"),
            c = a.val();
        b.removeClass("tab-marker-arrow").addClass(c)
    }), $("#tab-icon-changer").on("change", function() {
        var a = $(this),
            b = $(".tab-marker"),
            c = a.val();
        removeClassRegEx(b, /^tab-marker-icon/), b.addClass(c)
    }), $("#tab-responsive-changer").on("change", function() {
        var a = $(this),
            b = "css/",
            c = a.val() + ".css";
        $("link#responsiveness").attr("href", b + c)
    }), $(".settings-container").on("click", ".menu-marker", function() {
        var a = $(this),
            b = a.find(".icon");
        b.children("i").hasClass("fa-gear") ? (b.children("i").removeClass("fa-gear fa-spin").addClass("fa-times"), a.parent().next().css("margin-top", "180px")) : (b.children("i").removeClass("fa-times").addClass("fa-gear fa-spin"), a.parent().next().css("margin-top", "10px")), a.parent().toggleClass("show-settings")
    })
});
/*
window.oncontextmenu = function(event) {
    event.preventDefault();
    event.stopPropagation();
    return false;
};*/