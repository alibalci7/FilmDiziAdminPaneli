! function (a, b) {
    "use strict";
    var c = {
        bMac: navigator.appVersion.indexOf("Mac") > -1,
        bMSIE: navigator.userAgent.indexOf("MSIE") > -1,
        bFF: navigator.userAgent.indexOf("Firefox") > -1,
        bCodeMirror: !! b
    }, d = function () {
            var a = function (a) {
                return function (b) {
                    return a === b
                }
            }, b = function (a, b) {
                    return a === b
                }, c = function () {
                    return !1
                }, d = function (a) {
                    return function () {
                        return !a.apply(a, arguments)
                    }
                }, e = function (a) {
                    return a
                };
            return {
                eq: a,
                eq2: b,
                fail: c,
                not: d,
                self: e
            }
        }(),
        e = function () {
            var a = function (a) {
                return a[0]
            }, b = function (a) {
                    return a[a.length - 1]
                }, c = function (a) {
                    return a.slice(0, a.length - 1)
                }, e = function (a) {
                    return a.slice(1)
                }, f = function (a, b) {
                    return b = b || d.self, a.reduce(function (a, c) {
                        return a + b(c)
                    }, 0)
                }, g = function (a) {
                    for (var b = [], c = -1, d = a.length; ++c < d;) b[c] = a[c];
                    return b
                }, h = function (c, d) {
                    if (0 === c.length) return [];
                    var f = e(c);
                    return f.reduce(function (a, c) {
                        var e = b(a);
                        return d(b(e), c) ? e[e.length] = c : a[a.length] = [c], a
                    }, [
                        [a(c)]
                    ])
                }, i = function (a) {
                    for (var b = [], c = 0, d = a.length; d > c; c++) a[c] && b.push(a[c]);
                    return b
                };
            return {
                head: a,
                last: b,
                initial: c,
                tail: e,
                sum: f,
                from: g,
                compact: i,
                clusterBy: h
            }
        }(),
        f = function () {
            var b = function (b) {
                return a.Deferred(function (a) {
                    var c = new FileReader;
                    c.onload = function (b) {
                        a.resolve(b.target.result)
                    }, c.onerror = function () {
                        a.reject(this)
                    }, c.readAsDataURL(b)
                }).promise()
            }, c = function (b) {
                    return a.Deferred(function (a) {
                        function c() {
                            e(), a.resolve(f)
                        }

                        function d() {
                            e(), a.reject(f)
                        }

                        function e() {
                            f.onload = null, f.onerror = null, f.onabort = null
                        }
                        var f = new Image;
                        f.onload = c, f.onerror = d, f.onabort = d, f.src = b
                    }).promise()
                };
            return {
                readFile: b,
                loadImage: c
            }
        }(),
        g = function () {
            var b = function (a) {
                return function (b) {
                    return b && b.nodeName === a
                }
            }, c = function (a) {
                    return a && /^P|^LI|^H[1-7]/.test(a.nodeName)
                }, f = function (a) {
                    return a && /^UL|^OL/.test(a.nodeName)
                }, h = function (b) {
                    return b && a(b).hasClass("note-editable")
                }, i = function (b) {
                    return b && a(b).hasClass("note-control-sizing")
                }, j = function (a, b) {
                    for (; a;) {
                        if (b(a)) return a;
                        a = a.parentNode
                    }
                    return null
                }, k = function (a, b) {
                    b = b || d.fail;
                    var c = [];
                    return j(a, function (a) {
                        return c.push(a), b(a)
                    }), c
                }, l = function (b, c) {
                    for (var d = k(b), e = c; e; e = e.parentNode)
                        if (a.inArray(e, d) > -1) return e;
                    return null
                }, m = function (a, b) {
                    var c = [],
                        d = !1,
                        e = !1,
                        f = function (g) {
                            if (g) {
                                if (g === a && (d = !0), d && !e && c.push(g), g === b) return e = !0, void 0;
                                for (var h = 0, i = g.childNodes.length; i > h; h++) f(g.childNodes[h])
                            }
                        };
                    return f(l(a, b)), c
                }, n = function (a, b) {
                    b = b || d.fail;
                    for (var c = []; a && (c.push(a), !b(a));) a = a.previousSibling;
                    return c
                }, o = function (a, b) {
                    b = b || d.fail;
                    for (var c = []; a && (c.push(a), !b(a));) a = a.nextSibling;
                    return c
                }, p = function (a, b) {
                    var c = b.nextSibling,
                        d = b.parentNode;
                    return c ? d.insertBefore(a, c) : d.appendChild(a), a
                }, q = function (b, c) {
                    return a.each(c, function (a, c) {
                        b.appendChild(c)
                    }), b
                }, r = b("#text"),
                s = function (a) {
                    return r(a) ? a.nodeValue.length : a.childNodes.length
                }, t = function (a) {
                    for (var b = 0; a = a.previousSibling;) b += 1;
                    return b
                }, u = function (b, c) {
                    var f = e.initial(k(c, d.eq(b)));
                    return a.map(f, t).reverse()
                }, v = function (a, b) {
                    for (var c = a, d = 0, e = b.length; e > d; d++) c = c.childNodes[b[d]];
                    return c
                }, w = function (a, b) {
                    if (0 === b) return a;
                    if (b >= s(a)) return a.nextSibling;
                    if (r(a)) return a.splitText(b);
                    var c = a.childNodes[b];
                    return a = p(a.cloneNode(!1), a), q(a, o(c))
                }, x = function (a, b, c) {
                    var e = k(b, d.eq(a));
                    return 1 === e.length ? w(b, c) : e.reduce(function (a, d) {
                        var e = d.cloneNode(!1);
                        return p(e, d), a === b && (a = w(a, c)), q(e, o(a)), e
                    })
                }, y = function (a, b) {
                    if (a && a.parentNode) {
                        if (a.removeNode) return a.removeNode(b);
                        var c = a.parentNode;
                        if (!b) {
                            var d, e, f = [];
                            for (d = 0, e = a.childNodes.length; e > d; d++) f.push(a.childNodes[d]);
                            for (d = 0, e = f.length; e > d; d++) c.insertBefore(f[d], a)
                        }
                        c.removeChild(a)
                    }
                }, z = function (a) {
                    return g.isTextarea(a[0]) ? a.val() : a.html()
                };
            return {
                isText: r,
                isPara: c,
                isList: f,
                isEditable: h,
                isControlSizing: i,
                isAnchor: b("A"),
                isDiv: b("DIV"),
                isSpan: b("SPAN"),
                isB: b("B"),
                isU: b("U"),
                isS: b("S"),
                isI: b("I"),
                isImg: b("IMG"),
                isTextarea: b("TEXTAREA"),
                ancestor: j,
                listAncestor: k,
                listNext: o,
                listPrev: n,
                commonAncestor: l,
                listBetween: m,
                insertAfter: p,
                position: t,
                makeOffsetPath: u,
                fromOffsetPath: v,
                split: x,
                remove: y,
                html: z
            }
        }(),
        h = function () {
            var b = !! document.createRange,
                c = function (a, b) {
                    var c, d, f = a.parentElement(),
                        h = document.body.createTextRange(),
                        i = e.from(f.childNodes);
                    for (c = 0; c < i.length; c++)
                        if (!g.isText(i[c])) {
                            if (h.moveToElementText(i[c]), h.compareEndPoints("StartToStart", a) >= 0) break;
                            d = i[c]
                        }
                    if (0 !== c && g.isText(i[c - 1])) {
                        var j = document.body.createTextRange(),
                            k = null;
                        j.moveToElementText(d || f), j.collapse(!d), k = d ? d.nextSibling : f.firstChild;
                        var l = a.duplicate();
                        l.setEndPoint("StartToStart", j);
                        for (var m = l.text.replace(/[\r\n]/g, "").length; m > k.nodeValue.length && k.nextSibling;) m -= k.nodeValue.length, k = k.nextSibling; {
                            k.nodeValue
                        }
                        b && k.nextSibling && g.isText(k.nextSibling) && m === k.nodeValue.length && (m -= k.nodeValue.length, k = k.nextSibling), f = k, c = m
                    }
                    return {
                        cont: f,
                        offset: c
                    }
                }, f = function (a) {
                    var b = function (a, c) {
                        var f, h;
                        if (g.isText(a)) {
                            var i = g.listPrev(a, d.not(g.isText)),
                                j = e.last(i).previousSibling;
                            f = j || a.parentNode, c += e.sum(e.tail(i), g.length), h = !j
                        } else {
                            if (f = a.childNodes[c] || a, g.isText(f)) return b(f, c);
                            c = 0, h = !1
                        }
                        return {
                            cont: f,
                            collapseToStart: h,
                            offset: c
                        }
                    }, c = document.body.createTextRange(),
                        f = b(a.cont, a.offset);
                    return c.moveToElementText(f.cont), c.collapse(f.collapseToStart), c.moveStart("character", f.offset), c
                }, h = function (c, h, i, j) {
                    this.sc = c, this.so = h, this.ec = i, this.eo = j;
                    var k = function () {
                        if (b) {
                            var a = document.createRange();
                            return a.setStart(c, h), a.setEnd(i, j), a
                        }
                        var d = f({
                            cont: c,
                            offset: h
                        });
                        return d.setEndPoint("EndToEnd", f({
                            cont: i,
                            offset: j
                        })), d
                    };
                    this.select = function () {
                        var a = k();
                        if (b) {
                            var c = document.getSelection();
                            c.rangeCount > 0 && c.removeAllRanges(), c.addRange(a)
                        } else a.select()
                    }, this.listPara = function () {
                        var b = g.listBetween(c, i),
                            f = e.compact(a.map(b, function (a) {
                                return g.ancestor(a, g.isPara)
                            }));
                        return a.map(e.clusterBy(f, d.eq2), e.head)
                    };
                    var l = function (a) {
                        return function () {
                            var b = g.ancestor(c, a);
                            return b && b === g.ancestor(i, a)
                        }
                    };
                    this.isOnEditable = l(g.isEditable), this.isOnList = l(g.isList), this.isOnAnchor = l(g.isAnchor), this.isCollapsed = function () {
                        return c === i && h === j
                    }, this.insertNode = function (a) {
                        var c = k();
                        b ? c.insertNode(a) : c.pasteHTML(a.outerHTML)
                    }, this.toString = function () {
                        var a = k();
                        return b ? a.toString() : a.text
                    }, this.bookmark = function (a) {
                        return {
                            s: {
                                path: g.makeOffsetPath(a, c),
                                offset: h
                            },
                            e: {
                                path: g.makeOffsetPath(a, i),
                                offset: j
                            }
                        }
                    }
                };
            return {
                create: function (a, d, e, f) {
                    if (0 === arguments.length)
                        if (b) {
                            var g = document.getSelection();
                            if (0 === g.rangeCount) return null;
                            var i = g.getRangeAt(0);
                            a = i.startContainer, d = i.startOffset, e = i.endContainer, f = i.endOffset
                        } else {
                            var j = document.selection.createRange(),
                                k = j.duplicate();
                            k.collapse(!1);
                            var l = j;
                            l.collapse(!0);
                            var m = c(l, !0),
                                n = c(k, !1);
                            a = m.cont, d = m.offset, e = n.cont, f = n.offset
                        } else 2 === arguments.length && (e = a, f = d);
                    return new h(a, d, e, f)
                },
                createFromBookmark: function (a, b) {
                    var c = g.fromOffsetPath(a, b.s.path),
                        d = b.s.offset,
                        e = g.fromOffsetPath(a, b.e.path),
                        f = b.e.offset;
                    return new h(c, d, e, f)
                }
            }
        }(),
        i = function () {
            this.stylePara = function (b, c) {
                var d = b.listPara();
                a.each(d, function (b, d) {
                    a.each(c, function (a, b) {
                        d.style[a] = b
                    })
                })
            }, this.current = function (b, c) {
                var d = a(g.isText(b.sc) ? b.sc.parentNode : b.sc),
                    e = d.css(["font-size", "text-align", "list-style-type", "line-height"]) || {};
                if (e["font-size"] = parseInt(e["font-size"]), e["font-bold"] = document.queryCommandState("bold") ? "bold" : "normal", e["font-italic"] = document.queryCommandState("italic") ? "italic" : "normal", e["font-underline"] = document.queryCommandState("underline") ? "underline" : "normal", b.isOnList()) {
                    var f = ["circle", "disc", "disc-leading-zero", "square"],
                        h = a.inArray(e["list-style-type"], f) > -1;
                    e["list-style"] = h ? "unordered" : "ordered"
                } else e["list-style"] = "none";
                var i = g.ancestor(b.sc, g.isPara);
                if (i && i.style["line-height"]) e["line-height"] = i.style.lineHeight;
                else {
                    var j = parseInt(e["line-height"]) / parseInt(e["font-size"]);
                    e["line-height"] = j.toFixed(1)
                }
                return e.image = g.isImg(c) && c, e.anchor = b.isOnAnchor() && g.ancestor(b.sc, g.isAnchor), e.aAncestor = g.listAncestor(b.sc, g.isEditable), e
            }
        }, j = function () {
            var a = [],
                b = [],
                c = function (a) {
                    var b = a[0],
                        c = h.create();
                    return {
                        contents: a.html(),
                        bookmark: c.bookmark(b),
                        scrollTop: a.scrollTop()
                    }
                }, d = function (a, b) {
                    a.html(b.contents).scrollTop(b.scrollTop), h.createFromBookmark(a[0], b.bookmark).select()
                };
            this.undo = function (e) {
                var f = c(e);
                0 !== a.length && (d(e, a.pop()), b.push(f))
            }, this.redo = function (e) {
                var f = c(e);
                0 !== b.length && (d(e, b.pop()), a.push(f))
            }, this.recordUndo = function (d) {
                b = [], a.push(c(d))
            }
        }, k = function () {
            this.saveRange = function (a) {
                a.data("range", h.create())
            }, this.restoreRange = function (a) {
                var b = a.data("range");
                b && b.select()
            };
            var b = new i;
            this.currentStyle = function (a) {
                var c = h.create();
                return c.isOnEditable() && b.current(c, a)
            }, this.tab = function (b) {
                d(b);
                var c = h.create(),
                    e = new Array(b.data("tabsize") + 1).join("&nbsp;");
                c.insertNode(a('<span id="noteTab">' + e + "</span>")[0]);
                var f = a("#noteTab").removeAttr("id");
                c = h.create(f[0], 1), c.select(), g.remove(f[0])
            }, this.undo = function (a) {
                a.data("NoteHistory").undo(a)
            }, this.redo = function (a) {
                a.data("NoteHistory").redo(a)
            };
            for (var d = this.recordUndo = function (a) {
                a.data("NoteHistory").recordUndo(a)
            }, e = ["bold", "italic", "underline", "strikethrough", "justifyLeft", "justifyCenter", "justifyRight", "justifyFull", "insertOrderedList", "insertUnorderedList", "indent", "outdent", "formatBlock", "removeFormat", "backColor", "foreColor", "insertHorizontalRule"], j = 0, k = e.length; k > j; j++) this[e[j]] = function (a) {
                return function (b, c) {
                    d(b), document.execCommand(a, !1, c)
                }
            }(e[j]);
            this.insertImage = function (b, c) {
                f.loadImage(c).done(function (e) {
                    d(b);
                    var f = a("<img>").attr("src", c);
                    f.css("width", Math.min(b.width(), e.width)), h.create().insertNode(f[0])
                }).fail(function () {
                    var a = b.data("callbacks");
                    a.onImageUploadError && a.onImageUploadError()
                })
            }, this.formatBlock = function (a, b) {
                d(a), b = c.bMSIE ? "<" + b + ">" : b, document.execCommand("FormatBlock", !1, b)
            }, this.fontSize = function (a, b) {
                d(a), document.execCommand("fontSize", !1, 3), c.bFF ? a.find("font[size=3]").removeAttr("size").css("font-size", b + "px") : a.find("span").filter(function () {
                    return "medium" === this.style.fontSize
                }).css("font-size", b + "px")
            }, this.lineHeight = function (a, c) {
                d(a), b.stylePara(h.create(), {
                    lineHeight: c
                })
            }, this.unlink = function (a) {
                var b = h.create();
                if (b.isOnAnchor()) {
                    d(a);
                    var c = g.ancestor(b.sc, g.isAnchor);
                    b = h.create(c, 0, c, 1), b.select(), document.execCommand("unlink")
                }
            }, this.setLinkDialog = function (b, e) {
                var f = h.create();
                if (f.isOnAnchor()) {
                    var i = g.ancestor(f.sc, g.isAnchor);
                    f = h.create(i, 0, i, 1)
                }
                e({
                    range: f,
                    text: f.toString(),
                    url: f.isOnAnchor() ? g.ancestor(f.sc, g.isAnchor).href : ""
                }, function (e) {
                    f.select(), d(b);
                    var g = -1 !== e.toLowerCase().indexOf("://"),
                        i = g ? e : "http://" + e;
                    if (c.bMSIE && f.isCollapsed()) {
                        f.insertNode(a('<A id="linkAnchor">' + e + "</A>")[0]);
                        var j = a("#linkAnchor").removeAttr("id").attr("href", i);
                        f = h.create(j[0], 0, j[0], 1), f.select()
                    } else document.execCommand("createlink", !1, i)
                })
            }, this.color = function (a, b) {
                var c = JSON.parse(b),
                    e = c.foreColor,
                    f = c.backColor;
                d(a), e && document.execCommand("foreColor", !1, e), f && document.execCommand("backColor", !1, f)
            }, this.insertTable = function (b, e) {
                d(b);
                for (var f, g = e.split("x"), i = g[0], j = g[1], k = [], l = c.bMSIE ? "&nbsp;" : "<br/>", m = 0; i > m; m++) k.push("<td>" + l + "</td>");
                f = k.join("");
                for (var n, o = [], p = 0; j > p; p++) o.push("<tr>" + f + "</tr>");
                n = o.join("");
                var q = '<table class="table table-bordered">' + n + "</table>";
                h.create().insertNode(a(q)[0])
            }, this.floatMe = function (a, b, c) {
                d(a), c.style.cssFloat = b
            }, this.resize = function (a, b, c) {
                d(a), c.style.width = a.width() * b + "px", c.style.height = ""
            }, this.resizeTo = function (a, b) {
                var c = a.y / a.x,
                    d = b.data("ratio");
                b.css({
                    width: d > c ? a.x : a.y / d,
                    height: d > c ? a.x * d : a.y
                })
            }
        }, l = function () {
            this.update = function (b, c) {
                var d = function (b, c) {
                    b.find(".dropdown-menu li a").each(function () {
                        var b = a(this).attr("data-value") === c;
                        this.className = b ? "checked" : ""
                    })
                }, e = b.find(".note-fontsize");
                e.find(".note-current-fontsize").html(c["font-size"]), d(e, parseFloat(c["font-size"]));
                var f = b.find(".note-height");
                d(f, parseFloat(c["line-height"]));
                var g = function (a, c) {
                    var d = b.find(a);
                    d[c() ? "addClass" : "removeClass"]("active")
                };
                g('button[data-event="bold"]', function () {
                    return "bold" === c["font-bold"]
                }), g('button[data-event="italic"]', function () {
                    return "italic" === c["font-italic"]
                }), g('button[data-event="underline"]', function () {
                    return "underline" === c["font-underline"]
                }), g('button[data-event="justifyLeft"]', function () {
                    return "left" === c["text-align"] || "start" === c["text-align"]
                }), g('button[data-event="justifyCenter"]', function () {
                    return "center" === c["text-align"]
                }), g('button[data-event="justifyRight"]', function () {
                    return "right" === c["text-align"]
                }), g('button[data-event="justifyFull"]', function () {
                    return "justify" === c["text-align"]
                }), g('button[data-event="insertUnorderedList"]', function () {
                    return "unordered" === c["list-style"]
                }), g('button[data-event="insertOrderedList"]', function () {
                    return "ordered" === c["list-style"]
                })
            }, this.updateRecentColor = function (b, c, d) {
                var e = a(b).closest(".note-color"),
                    f = e.find(".note-recent-color"),
                    g = JSON.parse(f.attr("data-value"));
                g[c] = d, f.attr("data-value", JSON.stringify(g));
                var h = "backColor" === c ? "background-color" : "color";
                f.find("i").css(h, d)
            }, this.updateFullscreen = function (a, b) {
                var c = a.find('button[data-event="fullscreen"]');
                c[b ? "addClass" : "removeClass"]("active")
            }, this.updateCodeview = function (a, b) {
                var c = a.find('button[data-event="codeview"]');
                c[b ? "addClass" : "removeClass"]("active")
            }, this.enable = function (a) {
                a.find("button").not('button[data-event="codeview"]').removeClass("disabled")
            }, this.disable = function (a) {
                a.find("button").not('button[data-event="codeview"]').addClass("disabled")
            }
        }, m = function () {
            var b = function (b, c) {
                var d = a(c),
                    e = d.position(),
                    f = d.height();
                b.css({
                    display: "block",
                    left: e.left,
                    top: e.top + f
                })
            };
            this.update = function (a, c) {
                var d = a.find(".note-link-popover"),
                    e = a.find(".note-image-popover");
                if (c.anchor) {
                    var f = d.find("a");
                    f.attr("href", c.anchor.href).html(c.anchor.href), b(d, c.anchor)
                } else d.hide();
                c.image ? b(e, c.image) : e.hide()
            }, this.hide = function (a) {
                a.children().hide()
            }
        }, n = function () {
            this.update = function (b, c) {
                var d = b.find(".note-control-selection");
                if (c.image) {
                    var e = a(c.image),
                        f = e.position(),
                        g = {
                            w: e.width(),
                            h: e.height()
                        };
                    d.css({
                        display: "block",
                        left: f.left,
                        top: f.top,
                        width: g.w,
                        height: g.h
                    }).data("target", c.image);
                    var h = g.w + "x" + g.h;
                    d.find(".note-control-selection-info").text(h)
                } else d.hide()
            }, this.hide = function (a) {
                a.children().hide()
            }
        }, o = function () {
            this.showImageDialog = function (b, c, d, e) {
                var f = b.find(".note-image-dialog"),
                    g = b.find(".note-dropzone"),
                    h = b.find(".note-image-input"),
                    i = b.find(".note-image-url"),
                    j = b.find(".note-image-btn");
                f.on("shown.bs.modal", function () {
                    g.on("dragenter dragover dragleave", !1), g.on("drop", function (a) {
                        c(a), f.modal("hide")
                    }), h.on("change", function () {
                        d(this.files), a(this).val(""), f.modal("hide")
                    }), i.val("").keyup(function () {
                        i.val() ? j.removeClass("disabled").attr("disabled", !1) : j.addClass("disabled").attr("disabled", !0)
                    }).trigger("focus"), j.click(function (a) {
                        f.modal("hide"), e(i.val()), a.preventDefault()
                    })
                }).on("hidden.bs.modal", function () {
                    g.off("dragenter dragover dragleave drop"), h.off("change"), f.off("shown.bs.modal hidden.bs.modal"), i.off("keyup"), j.off("click")
                }).modal("show")
            }, this.showLinkDialog = function (a, b, c) {
                var d = a.find(".note-link-dialog"),
                    e = d.find(".note-link-text"),
                    f = d.find(".note-link-url"),
                    g = d.find(".note-link-btn");
                d.on("shown.bs.modal", function () {
                    e.html(b.text), f.val(b.url).keyup(function () {
                        f.val() ? g.removeClass("disabled").attr("disabled", !1) : g.addClass("disabled").attr("disabled", !0), b.text || e.html(f.val())
                    }).trigger("focus"), g.click(function (a) {
                        d.modal("hide"), c(f.val()), a.preventDefault()
                    })
                }).on("hidden.bs.modal", function () {
                    f.off("keyup"), g.off("click"), d.off("shown.bs.modal hidden.bs.modal")
                }).modal("show")
            }, this.showHelpDialog = function (a) {
                a.find(".note-help-dialog").modal("show")
            }
        }, p = function () {
            var d = new k,
                e = new l,
                h = new m,
                i = new n,
                j = new o,
                p = {
                    BACKSPACE: 8,
                    TAB: 9,
                    ENTER: 13,
                    SPACE: 32,
                    NUM0: 48,
                    NUM1: 49,
                    NUM6: 54,
                    NUM7: 55,
                    NUM8: 56,
                    B: 66,
                    E: 69,
                    I: 73,
                    J: 74,
                    K: 75,
                    L: 76,
                    R: 82,
                    S: 83,
                    U: 85,
                    Y: 89,
                    Z: 90,
                    SLASH: 191,
                    LEFTBRACKET: 219,
                    BACKSLACH: 220,
                    RIGHTBRACKET: 221
                }, q = function (b) {
                    var c = a(b).closest(".note-editor");
                    return {
                        editor: function () {
                            return c
                        },
                        toolbar: function () {
                            return c.find(".note-toolbar")
                        },
                        editable: function () {
                            return c.find(".note-editable")
                        },
                        codable: function () {
                            return c.find(".note-codable")
                        },
                        statusbar: function () {
                            return c.find(".note-statusbar")
                        },
                        popover: function () {
                            return c.find(".note-popover")
                        },
                        handle: function () {
                            return c.find(".note-handle")
                        },
                        dialog: function () {
                            return c.find(".note-dialog")
                        }
                    }
                }, r = function (a) {
                    var b = c.bMac ? a.metaKey : a.ctrlKey,
                        e = a.shiftKey,
                        f = a.keyCode,
                        g = b || e || f === p.TAB,
                        h = g ? q(a.target) : null;
                    if (f === p.TAB && h.editable().data("tabsize")) d.tab(h.editable());
                    else if (b && (e && f === p.Z || f === p.Y)) d.redo(h.editable());
                    else if (b && f === p.Z) d.undo(h.editable());
                    else if (b && f === p.B) d.bold(h.editable());
                    else if (b && f === p.I) d.italic(h.editable());
                    else if (b && f === p.U) d.underline(h.editable());
                    else if (b && e && f === p.S) d.strikethrough(h.editable());
                    else if (b && f === p.BACKSLACH) d.removeFormat(h.editable());
                    else if (b && f === p.K) h.editable(), d.setLinkDialog(h.editable(), function (a, b) {
                        j.showLinkDialog(h.dialog(), a, b)
                    });
                    else if (b && f === p.SLASH) j.showHelpDialog(h.dialog());
                    else if (b && e && f === p.L) d.justifyLeft(h.editable());
                    else if (b && e && f === p.E) d.justifyCenter(h.editable());
                    else if (b && e && f === p.R) d.justifyRight(h.editable());
                    else if (b && e && f === p.J) d.justifyFull(h.editable());
                    else if (b && e && f === p.NUM7) d.insertUnorderedList(h.editable());
                    else if (b && e && f === p.NUM8) d.insertOrderedList(h.editable());
                    else if (b && f === p.LEFTBRACKET) d.outdent(h.editable());
                    else if (b && f === p.RIGHTBRACKET) d.indent(h.editable());
                    else if (b && f === p.NUM0) d.formatBlock(h.editable(), "P");
                    else if (b && p.NUM1 <= f && f <= p.NUM6) {
                        var i = "H" + String.fromCharCode(f);
                        d.formatBlock(h.editable(), i)
                    } else {
                        if (!b || f !== p.ENTER) return (f === p.BACKSPACE || f === p.ENTER || f === p.SPACE) && d.recordUndo(q(a.target).editable()), void 0;
                        d.insertHorizontalRule(h.editable())
                    }
                    a.preventDefault()
                }, s = function (b, c) {
                    var e = b.data("callbacks");
                    d.restoreRange(b), e.onImageUpload ? e.onImageUpload(c, d, b) : a.each(c, function (a, c) {
                        f.readFile(c).done(function (a) {
                            d.insertImage(b, a)
                        }).fail(function () {
                            e.onImageUploadError && e.onImageUploadError()
                        })
                    })
                }, t = function (a) {
                    var b = a.originalEvent.dataTransfer;
                    if (b && b.files) {
                        var c = q(a.currentTarget || a.target);
                        s(c.editable(), b.files)
                    }
                    a.stopPropagation(), a.preventDefault()
                }, u = function (a) {
                    g.isImg(a.target) && a.preventDefault()
                }, v = function (a) {
                    var b = q(a.currentTarget || a.target),
                        c = d.currentStyle(a.target);
                    c && (e.update(b.toolbar(), c), h.update(b.popover(), c), i.update(b.handle(), c))
                }, w = function (a) {
                    var b = q(a.currentTarget || a.target);
                    h.hide(b.popover()), i.hide(b.handle())
                }, x = function (b) {
                    if (g.isControlSizing(b.target)) {
                        var c, e = q(b.target),
                            f = e.handle(),
                            j = e.popover(),
                            k = e.editable(),
                            l = e.editor(),
                            m = f.find(".note-control-selection").data("target"),
                            n = a(m),
                            o = n.offset(),
                            p = a(document).scrollTop();
                        l.on("mousemove", function (a) {
                            c = {
                                x: a.clientX - o.left,
                                y: a.clientY - (o.top - p)
                            }, d.resizeTo(c, n), i.update(f, {
                                image: m
                            }), h.update(j, {
                                image: m
                            })
                        }).on("mouseup", function () {
                            l.off("mousemove").off("mouseup")
                        }), n.data("ratio") || n.data("ratio", n.height() / n.width()), d.recordUndo(k), b.stopPropagation(), b.preventDefault()
                    }
                }, y = function (b) {
                    var c = a(b.target).closest("[data-event]");
                    c.length > 0 && b.preventDefault()
                }, z = function (f) {
                    var g = a(f.target).closest("[data-event]");
                    if (g.length > 0) {
                        var h, i = g.attr("data-event"),
                            k = g.attr("data-value"),
                            l = q(f.target),
                            m = l.editor(),
                            n = l.toolbar(),
                            o = l.dialog(),
                            p = l.editable(),
                            r = l.codable();
                        if (-1 !== a.inArray(i, ["resize", "floatMe"])) {
                            var u = l.handle(),
                                w = u.find(".note-control-selection");
                            h = w.data("target")
                        }
                        if (d[i] && (p.trigger("focus"), d[i](p, k, h)), -1 !== a.inArray(i, ["backColor", "foreColor"])) e.updateRecentColor(g[0], i, k);
                        else if ("showLinkDialog" === i) p.focus(), d.setLinkDialog(p, function (a, b) {
                            j.showLinkDialog(o, a, b)
                        });
                        else if ("showImageDialog" === i) p.focus(), j.showImageDialog(o, t, function (a) {
                            s(p, a)
                        }, function (a) {
                            d.restoreRange(p), d.insertImage(p, a)
                        });
                        else if ("showHelpDialog" === i) j.showHelpDialog(o);
                        else if ("fullscreen" === i) {
                            m.toggleClass("fullscreen");
                            var x = function () {
                                var b = a(window).height() - n.outerHeight();
                                p.css("height", b)
                            }, y = m.hasClass("fullscreen");
                            if (y) p.data("orgHeight", p.css("height")), a(window).resize(x).trigger("resize");
                            else {
                                var z = !! p.data("optionHeight");
                                p.css("height", z ? p.data("orgHeight") : "auto"), a(window).off("resize")
                            }
                            e.updateFullscreen(n, y)
                        } else if ("codeview" === i) {
                            m.toggleClass("codeview");
                            var A = m.hasClass("codeview");
                            if (A) {
                                if (r.val(p.html()), r.height(p.height()), e.disable(n), r.focus(), c.bCodeMirror) {
                                    var B = b.fromTextArea(r[0], {
                                        mode: "text/html",
                                        lineNumbers: !0
                                    });
                                    B.setSize(null, p.outerHeight()), B.autoFormatRange && B.autoFormatRange({
                                        line: 0,
                                        ch: 0
                                    }, {
                                        line: B.lineCount(),
                                        ch: B.getTextArea().value.length
                                    }), r.data("cmEditor", B)
                                }
                            } else c.bCodeMirror && r.data("cmEditor").toTextArea(), p.html(r.val()), p.height(p.data("optionHeight") ? r.height() : "auto"), e.enable(n), p.focus();
                            e.updateCodeview(l.toolbar(), A)
                        }
                        v(f)
                    }
                }, A = 24,
                B = function (b) {
                    var c = a(document),
                        d = q(b.target),
                        e = d.editable(),
                        f = e.offset().top - c.scrollTop(),
                        g = function (a) {
                            e.height(a.clientY - (f + A))
                        }, h = function () {
                            c.unbind("mousemove", g).unbind("mouseup", h)
                        };
                    c.mousemove(g).mouseup(h), b.stopPropagation(), b.preventDefault()
                }, C = 18,
                D = function (b) {
                    var c, d = a(b.target.parentNode),
                        e = d.next(),
                        f = d.find(".note-dimension-picker-mousecatcher"),
                        g = d.find(".note-dimension-picker-highlighted"),
                        h = d.find(".note-dimension-picker-unhighlighted");
                    if (void 0 === b.offsetX) {
                        var i = a(b.target).offset();
                        c = {
                            x: b.pageX - i.left,
                            y: b.pageY - i.top
                        }
                    } else c = {
                        x: b.offsetX,
                        y: b.offsetY
                    };
                    var j = {
                        c: Math.ceil(c.x / C) || 1,
                        r: Math.ceil(c.y / C) || 1
                    };
                    g.css({
                        width: j.c + "em",
                        height: j.r + "em"
                    }), f.attr("data-value", j.c + "x" + j.r), 3 < j.c && j.c < 10 && h.css({
                        width: j.c + 1 + "em"
                    }), 3 < j.r && j.r < 10 && h.css({
                        height: j.r + 1 + "em"
                    }), e.html(j.c + " x " + j.r)
                };
            this.attach = function (a, b) {
                a.editable.on("keydown", r), a.editable.on("mousedown", u), a.editable.on("keyup mouseup", v), a.editable.on("scroll", w), a.editable.on("dragenter dragover dragleave", !1), a.editable.on("drop", t), a.handle.on("mousedown", x), a.toolbar.on("click", z), a.popover.on("click", z), a.toolbar.on("mousedown", y), a.popover.on("mousedown", y), a.statusbar.on("mousedown", B);
                var c = a.toolbar,
                    e = c.find(".note-dimension-picker-mousecatcher");
                e.on("mousemove", D), a.editable.on("blur", function () {
                    d.saveRange(a.editable)
                }), b.onenter && a.editable.keypress(function (a) {
                    a.keyCode === p.ENTER && b.onenter(a)
                }), b.onfocus && a.editable.focus(b.onfocus), b.onblur && a.editable.blur(b.onblur), b.onkeyup && a.editable.keyup(b.onkeyup), b.onkeydown && a.editable.keydown(b.onkeydown), a.editable.data("callbacks", {
                    onChange: b.onChange,
                    onAutoSave: b.onAutoSave,
                    onPasteBefore: b.onPasteBefore,
                    onPasteAfter: b.onPasteAfter,
                    onImageUpload: b.onImageUpload,
                    onImageUploadError: b.onImageUpload,
                    onFileUpload: b.onFileUpload,
                    onFileUploadError: b.onFileUpload
                })
            }, this.dettach = function (a) {
                a.editable.off(), a.toolbar.off(), a.handle.off(), a.popover.off()
            }
        }, q = function () {
            var b = {
                picture: '<button type="button" class="btn btn-default btn-sm btn-small" title="Picture" data-event="showImageDialog" tabindex="-1"><i class="fa fa-picture-o icon-picture"></i></button>',
                link: '<button type="button" class="btn btn-default btn-sm btn-small" title="Link" data-event="showLinkDialog" data-shortcut="Ctrl+K" data-mac-shortcut="???+K" tabindex="-1"><i class="fa fa-link icon-link"></i></button>',
                table: '<button type="button" class="btn btn-default btn-sm btn-small dropdown-toggle" title="Table" data-toggle="dropdown" tabindex="-1"><i class="fa fa-table icon-table"></i> <span class="caret"></span></button><ul class="dropdown-menu"><div class="note-dimension-picker"><div class="note-dimension-picker-mousecatcher" data-event="insertTable" data-value="1x1"></div><div class="note-dimension-picker-highlighted"></div><div class="note-dimension-picker-unhighlighted"></div></div><div class="note-dimension-display"> 1 x 1 </div></ul>',
                style: '<button type="button" class="btn btn-default btn-sm btn-small dropdown-toggle" title="Style" data-toggle="dropdown" tabindex="-1"><i class="fa fa-magic icon-magic"></i> <span class="caret"></span></button><ul class="dropdown-menu"><li><a data-event="formatBlock" data-value="p">Normal</a></li><li><a data-event="formatBlock" data-value="blockquote"><blockquote>Quote</blockquote></a></li><li><a data-event="formatBlock" data-value="pre">Code</a></li><li><a data-event="formatBlock" data-value="h1"><h1>Header 1</h1></a></li><li><a data-event="formatBlock" data-value="h2"><h2>Header 2</h2></a></li><li><a data-event="formatBlock" data-value="h3"><h3>Header 3</h3></a></li><li><a data-event="formatBlock" data-value="h4"><h4>Header 4</h4></a></li><li><a data-event="formatBlock" data-value="h5"><h5>Header 5</h5></a></li><li><a data-event="formatBlock" data-value="h6"><h6>Header 6</h6></a></li></ul>',
                fontsize: '<button type="button" class="btn btn-default btn-sm btn-small dropdown-toggle" data-toggle="dropdown" title="Font Size" tabindex="-1"><span class="note-current-fontsize">11</span> <b class="caret"></b></button><ul class="dropdown-menu"><li><a data-event="fontSize" data-value="8"><i class="fa fa-check icon-ok"></i> 8</a></li><li><a data-event="fontSize" data-value="9"><i class="fa fa-check icon-ok"></i> 9</a></li><li><a data-event="fontSize" data-value="10"><i class="fa fa-check icon-ok"></i> 10</a></li><li><a data-event="fontSize" data-value="11"><i class="fa fa-check icon-ok"></i> 11</a></li><li><a data-event="fontSize" data-value="12"><i class="fa fa-check icon-ok"></i> 12</a></li><li><a data-event="fontSize" data-value="14"><i class="fa fa-check icon-ok"></i> 14</a></li><li><a data-event="fontSize" data-value="18"><i class="fa fa-check icon-ok"></i> 18</a></li><li><a data-event="fontSize" data-value="24"><i class="fa fa-check icon-ok"></i> 24</a></li><li><a data-event="fontSize" data-value="36"><i class="fa fa-check icon-ok"></i> 36</a></li></ul>',
                color: '<button type="button" class="btn btn-default btn-sm btn-small note-recent-color" title="Recent Color" data-event="color" data-value=\'{"backColor":"yellow"}\' tabindex="-1"><i class="fa fa-font icon-font" style="color:black;background-color:yellow;"></i></button><button type="button" class="btn btn-default btn-sm btn-small dropdown-toggle" title="More Color" data-toggle="dropdown" tabindex="-1"><span class="caret"></span></button><ul class="dropdown-menu"><li><div class="btn-group"><div class="note-palette-title">BackColor</div><div class="note-color-reset" data-event="backColor" data-value="inherit" title="Transparent">Set transparent</div><div class="note-color-palette" data-target-event="backColor"></div></div><div class="btn-group"><div class="note-palette-title">FontColor</div><div class="note-color-reset" data-event="foreColor" data-value="inherit" title="Reset">Reset to default</div><div class="note-color-palette" data-target-event="foreColor"></div></div></li></ul>',
                bold: '<button type="button" class="btn btn-default btn-sm btn-small" title="Bold" data-shortcut="Ctrl+B" data-mac-shortcut="???+B" data-event="bold" tabindex="-1"><i class="fa fa-bold icon-bold"></i></button>',
                italic: '<button type="button" class="btn btn-default btn-sm btn-small" title="Italic" data-shortcut="Ctrl+I" data-mac-shortcut="???+I" data-event="italic" tabindex="-1"><i class="fa fa-italic icon-italic"></i></button>',
                underline: '<button type="button" class="btn btn-default btn-sm btn-small" title="Underline" data-shortcut="Ctrl+U" data-mac-shortcut="???+U" data-event="underline" tabindex="-1"><i class="fa fa-underline icon-underline"></i></button>',
                clear: '<button type="button" class="btn btn-default btn-sm btn-small" title="Remove Font Style" data-shortcut="Ctrl+\\" data-mac-shortcut="???+\\" data-event="removeFormat" tabindex="-1"><i class="fa fa-eraser icon-eraser"></i></button>',
                ul: '<button type="button" class="btn btn-default btn-sm btn-small" title="Unordered list" data-shortcut="Ctrl+Shift+8" data-mac-shortcut="???+???+7" data-event="insertUnorderedList" tabindex="-1"><i class="fa fa-list-ul icon-list-ul"></i></button>',
                ol: '<button type="button" class="btn btn-default btn-sm btn-small" title="Ordered list" data-shortcut="Ctrl+Shift+7" data-mac-shortcut="???+???+8" data-event="insertOrderedList" tabindex="-1"><i class="fa fa-list-ol icon-list-ol"></i></button>',
                paragraph: '<button type="button" class="btn btn-default btn-sm btn-small dropdown-toggle" title="Paragraph" data-toggle="dropdown" tabindex="-1"><i class="fa fa-align-left icon-align-left"></i>  <span class="caret"></span></button><ul class="dropdown-menu"><li><div class="note-align btn-group"><button type="button" class="btn btn-default btn-sm btn-small" title="Align left" data-shortcut="Ctrl+Shift+L" data-mac-shortcut="???+???+L" data-event="justifyLeft" tabindex="-1"><i class="fa fa-align-left icon-align-left"></i></button><button type="button" class="btn btn-default btn-sm btn-small" title="Align center" data-shortcut="Ctrl+Shift+E" data-mac-shortcut="???+???+E" data-event="justifyCenter" tabindex="-1"><i class="fa fa-align-center icon-align-center"></i></button><button type="button" class="btn btn-default btn-sm btn-small" title="Align right" data-shortcut="Ctrl+Shift+R" data-mac-shortcut="???+???+R" data-event="justifyRight" tabindex="-1"><i class="fa fa-align-right icon-align-right"></i></button><button type="button" class="btn btn-default btn-sm btn-small" title="Justify full" data-shortcut="Ctrl+Shift+J" data-mac-shortcut="???+???+J" data-event="justifyFull" tabindex="-1"><i class="fa fa-align-justify icon-align-justify"></i></button></div></li><li><div class="note-list btn-group"><button type="button" class="btn btn-default btn-sm btn-small" title="Outdent" data-shortcut="Ctrl+[" data-mac-shortcut="???+[" data-event="outdent" tabindex="-1"><i class="fa fa-outdent icon-indent-left"></i></button><button type="button" class="btn btn-default btn-sm btn-small" title="Indent" data-shortcut="Ctrl+]" data-mac-shortcut="???+]" data-event="indent" tabindex="-1"><i class="fa fa-indent icon-indent-right"></i></button></li></ul>',
                height: '<button type="button" class="btn btn-default btn-sm btn-small dropdown-toggle" data-toggle="dropdown" title="Line Height" tabindex="-1"><i class="fa fa-text-height icon-text-height"></i>&nbsp; <b class="caret"></b></button><ul class="dropdown-menu"><li><a data-event="lineHeight" data-value="1.0"><i class="fa fa-check icon-ok"></i> 1.0</a></li><li><a data-event="lineHeight" data-value="1.2"><i class="fa fa-check icon-ok"></i> 1.2</a></li><li><a data-event="lineHeight" data-value="1.4"><i class="fa fa-check icon-ok"></i> 1.4</a></li><li><a data-event="lineHeight" data-value="1.5"><i class="fa fa-check icon-ok"></i> 1.5</a></li><li><a data-event="lineHeight" data-value="1.6"><i class="fa fa-check icon-ok"></i> 1.6</a></li><li><a data-event="lineHeight" data-value="1.8"><i class="fa fa-check icon-ok"></i> 1.8</a></li><li><a data-event="lineHeight" data-value="2.0"><i class="fa fa-check icon-ok"></i> 2.0</a></li><li><a data-event="lineHeight" data-value="3.0"><i class="fa fa-check icon-ok"></i> 3.0</a></li></ul>',
                help: '<button type="button" class="btn btn-default btn-sm btn-small" title="Help" data-shortcut="Ctrl+/" data-mac-shortcut="???+/" data-event="showHelpDialog" tabindex="-1"><i class="fa fa-question icon-question"></i></button>',
                fullscreen: '<button type="button" class="btn btn-default btn-sm btn-small" title="Full Screen" data-event="fullscreen" tabindex="-1"><i class="fa fa-arrows-alt icon-fullscreen"></i></button>',
                codeview: '<button type="button" class="btn btn-default btn-sm btn-small" title="Code View" data-event="codeview" tabindex="-1"><i class="fa fa-code icon-code"></i></button>'
            }, d = '<div class="note-popover"><div class="note-link-popover popover bottom in" style="display: none;"><div class="arrow"></div><div class="popover-content note-link-content"><a href="http://www.google.com" target="_blank">www.google.com</a>&nbsp;&nbsp;<div class="note-insert btn-group"><button type="button" class="btn btn-default btn-sm btn-small" title="Edit" data-event="showLinkDialog" tabindex="-1"><i class="fa fa-edit icon-edit"></i></button><button type="button" class="btn btn-default btn-sm btn-small" title="Unlink" data-event="unlink" tabindex="-1"><i class="fa fa-unlink icon-unlink"></i></button></div></div></div><div class="note-image-popover popover bottom in" style="display: none;"><div class="arrow"></div><div class="popover-content note-image-content"><div class="btn-group"><button type="button" class="btn btn-default btn-sm btn-small" title="Resize Full" data-event="resize" data-value="1" tabindex="-1"><span class="note-fontsize-10">100%</span> </button><button type="button" class="btn btn-default btn-sm btn-small" title="Resize Half" data-event="resize" data-value="0.5" tabindex="-1"><span class="note-fontsize-10">50%</span> </button><button type="button" class="btn btn-default btn-sm btn-small" title="Resize Quarter" data-event="resize" data-value="0.25" tabindex="-1"><span class="note-fontsize-10">25%</span> </button></div><div class="btn-group"><button type="button" class="btn btn-default btn-sm btn-small" title="Float Left" data-event="floatMe" data-value="left" tabindex="-1"><i class="fa fa-align-left icon-align-left"></i></button><button type="button" class="btn btn-default btn-sm btn-small" title="Float Right" data-event="floatMe" data-value="right" tabindex="-1"><i class="fa fa-align-right icon-align-right"></i></button><button type="button" class="btn btn-default btn-sm btn-small" title="Float None" data-event="floatMe" data-value="none" tabindex="-1"><i class="fa fa-align-justify icon-align-justify"></i></button></div></div></div></div>',
                e = '<div class="note-handle"><div class="note-control-selection"><div class="note-control-selection-bg"></div><div class="note-control-holder note-control-nw"></div><div class="note-control-holder note-control-ne"></div><div class="note-control-holder note-control-sw"></div><div class="note-control-sizing note-control-se"></div><div class="note-control-selection-info"></div></div></div>',
                f = '<table class="note-shortcut"><thead><tr><th></th><th>Text formatting</th></tr></thead><tbody><tr><td>??? + B</td><td>Toggle Bold</td></tr><tr><td>??? + I</td><td>Toggle Italic</td></tr><tr><td>??? + U</td><td>Toggle Underline</td></tr><tr><td>??? + ??? + S</td><td>Toggle Strike</td></tr><tr><td>??? + \\</td><td>Remove Font Style</td></tr></tr></tbody></table>',
                h = '<table class="note-shortcut"><thead><tr><th></th><th>Action</th></tr></thead><tbody><tr><td>??? + Z</td><td>Undo</td></tr><tr><td>??? + ??? + Z</td><td>Redo</td></tr><tr><td>??? + ]</td><td>Indent</td></tr><tr><td>??? + [</td><td>Outdent</td></tr><tr><td>??? + K</td><td>Insert Link</td></tr><tr><td>??? + ENTER</td><td>Insert Horizontal Rule</td></tr></tbody></table>',
                i = '<table class="note-shortcut"><thead><tr><th></th><th>Paragraph formatting</th></tr></thead><tbody><tr><td>??? + ??? + L</td><td>Align Left</td></tr><tr><td>??? + ??? + E</td><td>Align Center</td></tr><tr><td>??? + ??? + R</td><td>Align Right</td></tr><tr><td>??? + ??? + J</td><td>Justify Full</td></tr><tr><td>??? + ??? + NUM7</td><td>Ordered List</td></tr><tr><td>??? + ??? + NUM8</td><td>Unordered List</td></tr></tbody></table>',
                k = '<table class="note-shortcut"><thead><tr><th></th><th>Document Style</th></tr></thead><tbody><tr><td>??? + NUM0</td><td>Normal Text</td></tr><tr><td>??? + NUM1</td><td>Heading 1</td></tr><tr><td>??? + NUM2</td><td>Heading 2</td></tr><tr><td>??? + NUM3</td><td>Heading 3</td></tr><tr><td>??? + NUM4</td><td>Heading 4</td></tr><tr><td>??? + NUM5</td><td>Heading 5</td></tr><tr><td>??? + NUM6</td><td>Heading 6</td></tr></tbody></table>',
                l = '<table class="note-shortcut-layout"><tbody><tr><td>' + h + "</td><td>" + f + "</td></tr><tr><td>" + k + "</td><td>" + i + "</td></tr></tbody></table>";
            c.bMac || (l = l.replace(/???/g, "Ctrl").replace(/???/g, "Shift"));
            var m = '<div class="note-dialog"><div class="note-image-dialog modal" aria-hidden="false"><div class="modal-dialog"><div class="modal-content"><div class="modal-header"><button type="button" class="close" aria-hidden="true" tabindex="-1">??</button><h4>Insert Image</h4></div><div class="modal-body"><div class="row-fluid"><div class="note-dropzone span12">Drag an image here</div><h5>Select from files</h5><input class="note-image-input" type="file" name="files" accept="image/*" capture="camera" /><h5>Image URL</h5><input class="note-image-url form-control span12" type="text" /></div></div><div class="modal-footer"><button href="#" class="btn btn-primary note-image-btn disabled" disabled="disabled">Insert</button></div></div></div></div><div class="note-link-dialog modal" aria-hidden="false"><div class="modal-dialog"><div class="modal-content"><div class="modal-header"><button type="button" class="close" aria-hidden="true" tabindex="-1">??</button><h4>Edit Link</h4></div><div class="modal-body"><div class="row-fluid"><div class="form-group"><label>Text to display</label><span class="note-link-text form-control input-xlarge uneditable-input" /></div><div class="form-group"><label>To what URL should this link go?</label><input class="note-link-url form-control span12" type="text" /></div></div></div><div class="modal-footer"><button href="#" class="btn btn-primary note-link-btn disabled" disabled="disabled">Link</button></div></div></div></div><div class="note-help-dialog modal" aria-hidden="false"><div class="modal-dialog"><div class="modal-content"><div class="modal-body"><div class="modal-background"><a class="modal-close pull-right" aria-hidden="true" tabindex="-1">Close</a><div class="title">Keyboard shortcuts</div>' + l + '<p class="text-center"><a href="//hackerwins.github.io/summernote/" target="_blank">Summernote v0.4</a> ?? <a href="//github.com/HackerWins/summernote" target="_blank">Project</a> ?? <a href="//github.com/HackerWins/summernote/issues" target="_blank">Issues</a></p></div></div></div></div></div>',
                n = '<div class="note-resizebar"><div class="note-icon-bar"></div><div class="note-icon-bar"></div><div class="note-icon-bar"></div></div>',
                o = function (b, d) {
                    b.find("button").each(function (b, d) {
                        var e = a(d),
                            f = e.attr(c.bMac ? "data-mac-shortcut" : "data-shortcut");
                        f && e.attr("title", function (a, b) {
                            return b + " (" + f + ")"
                        })
                    }).tooltip({
                        container: "body",
                        placement: d || "top"
                    })
                }, p = [
                    ["#000000", "#424242", "#636363", "#9C9C94", "#CEC6CE", "#EFEFEF", "#F7F7F7", "#FFFFFF"],
                    ["#FF0000", "#FF9C00", "#FFFF00", "#00FF00", "#00FFFF", "#0000FF", "#9C00FF", "#FF00FF"],
                    ["#F7C6CE", "#FFE7CE", "#FFEFC6", "#D6EFD6", "#CEDEE7", "#CEE7F7", "#D6D6E7", "#E7D6DE"],
                    ["#E79C9C", "#FFC69C", "#FFE79C", "#B5D6A5", "#A5C6CE", "#9CC6EF", "#B5A5D6", "#D6A5BD"],
                    ["#E76363", "#F7AD6B", "#FFD663", "#94BD7B", "#73A5AD", "#6BADDE", "#8C7BC6", "#C67BA5"],
                    ["#CE0000", "#E79439", "#EFC631", "#6BA54A", "#4A7B8C", "#3984C6", "#634AA5", "#A54A7B"],
                    ["#9C0000", "#B56308", "#BD9400", "#397B21", "#104A5A", "#085294", "#311873", "#731842"],
                    ["#630000", "#7B3900", "#846300", "#295218", "#083139", "#003163", "#21104A", "#4A1031"]
                ],
                q = function (b) {
                    b.find(".note-color-palette").each(function () {
                        for (var b = a(this), c = b.attr("data-target-event"), d = "", e = 0, f = p.length; f > e; e++) {
                            for (var g = p[e], h = "<div>", i = 0, j = g.length; j > i; i++) {
                                var k = g[i],
                                    l = ['<button type="button" class="note-color-btn" style="background-color:', k, ';" data-event="', c, '" data-value="', k, '" title="', k, '" data-toggle="button" tabindex="-1"></button>'].join("");
                                h += l
                            }
                            h += "</div>", d += h
                        }
                        b.html(d)
                    })
                };
            this.createLayout = function (c, f, h, i) {
                if (!c.next().hasClass("note-editor")) {
                    var k = a('<div class="note-editor"></div>');
                    f > 0 && a('<div class="note-statusbar">' + n + "</div>").prependTo(k);
                    var l = a('<div class="note-editable" contentEditable="true"></div>').prependTo(k);
                    f && (l.height(f), l.data("optionHeight", f)), h && l.data("tabsize", h), l.html(g.html(c)), l.data("NoteHistory", new j), a('<textarea class="note-codable"></textarea>').prependTo(k), setTimeout(function () {
                        document.execCommand("styleWithCSS", 0, !0)
                    });
                    for (var p = "", r = 0, s = i.length; s > r; r++) {
                        var t = i[r];
                        p += '<div class="note-' + t[0] + ' btn-group">';
                        for (var u = 0, v = t[1].length; v > u; u++) p += b[t[1][u]];
                        p += "</div>"
                    }
                    p = '<div class="note-toolbar btn-toolbar">' + p + "</div>";
                    var w = a(p).prependTo(k);
                    q(w), o(w, "bottom");
                    var x = a(d).prependTo(k);
                    o(x), a(e).prependTo(k);
                    var y = a(m).prependTo(k);
                    y.find("button.close, a.modal-close").click(function () {
                        a(this).closest(".modal").modal("hide")
                    }), k.insertAfter(c), c.hide()
                }
            };
            var r = this.layoutInfoFromHolder = function (a) {
                var b = a.next();
                if (b.hasClass("note-editor")) return {
                    editor: b,
                    toolbar: b.find(".note-toolbar"),
                    editable: b.find(".note-editable"),
                    codable: b.find(".note-codable"),
                    statusbar: b.find(".note-statusbar"),
                    popover: b.find(".note-popover"),
                    handle: b.find(".note-handle"),
                    dialog: b.find(".note-dialog")
                }
            };
            this.removeLayout = function (a) {
                var b = r(a);
                b && (a.html(b.editable.html()), b.editor.remove(), a.show())
            }
        }, r = new q,
        s = new p;
    a.fn.extend({
        summernote: function (b) {
            if (b = a.extend({
                toolbar: [
                    ["style", ["style"]],
                    ["font", ["bold", "italic", "underline", "clear"]],
                    ["fontsize", ["fontsize"]],
                    ["color", ["color"]],
                    ["para", ["ul", "ol", "paragraph"]],
                    ["height", ["height"]],
                    ["table", ["table"]],
                    ["insert", ["link", "picture"]],
                    ["view", ["fullscreen", "codeview"]],
                    ["help", ["help"]]
                ]
            }, b), this.each(function (c, d) {
                var e = a(d);
                r.createLayout(e, b.height, b.tabsize, b.toolbar);
                var f = r.layoutInfoFromHolder(e);
                s.attach(f, b)
            }), this.first() && b.focus) {
                var c = r.layoutInfoFromHolder(this.first());
                c.editable.focus()
            }
            this.length > 0 && b.oninit && b.oninit()
        },
        code: function (b) {
            if (void 0 === b) {
                var d = this.first();
                if (0 === d.length) return;
                var e = r.layoutInfoFromHolder(d);
                if (e && e.editable) {
                    var f = e.editor.hasClass("codeview");
                    return c.bCodeMirror && e.codable.data("cmEditor").save(), f ? e.codable.val() : e.editable.html()
                }
                return d.html()
            }
            this.each(function (c, d) {
                var e = r.layoutInfoFromHolder(a(d));
                e && e.editable && e.editable.html(b)
            })
        },
        destroy: function () {
            this.each(function (b, c) {
                var d = a(c),
                    e = r.layoutInfoFromHolder(d);
                e && e.editable && (s.dettach(e), r.removeLayout(d))
            })
        },
        summernoteInner: function () {
            return {
                dom: g,
                list: e,
                func: d,
                range: h
            }
        }
    })
}(window.jQuery, window.CodeMirror), "function" != typeof Array.prototype.reduce && (Array.prototype.reduce = function (a, b) {
    "use strict";
    var c, d, e = this.length >>> 0,
        f = !1;
    for (1 < arguments.length && (d = b, f = !0), c = 0; e > c; ++c) this.hasOwnProperty(c) && (f ? d = a(d, this[c], c, this) : (d = this[c], f = !0));
    if (!f) throw new TypeError("Reduce of empty array with no initial value");
    return d
});