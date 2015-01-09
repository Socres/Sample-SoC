var EnableHighlighting = function () {

    var enable = function () {
        marked.setOptions({
            sanitize: false,
            highlight: function (code, lang) {
                var language = lang;
                if (!language) {
                    var languageRegex = new RegExp("^({{(.+)}}[\r\n])");
                    var match = code.match(languageRegex);
                    if (match) {
                        language = match[2].toLowerCase();
                        code = code.replace(languageRegex, "");
                    }
                }
                if (language == "c#" || language == "csharp") {
                    language = "cs";
                }
                if (language == "Javascript") {
                    language = "js";
                }
                if (language && hljs.LANGUAGES[language]) {
                    return hljs.highlight(language, code).value;
                } else {
                    return hljs.highlightAuto(code).value;
                }
            },
            breaks: true
        });
    };

    return {
        enable: enable
    }
}();
