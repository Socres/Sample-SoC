var BlogPostViewModel = function (blogPostId, url) {

    var blogId = blogPostId,
        serviceUrl = url,
        comments = ko.observableArray([]),
        getComments = function() {
            return $.getJSON(serviceUrl + 'blog/' + blogId)
                .done(function(data) {
                    // On success, 'data' contains a list of products.
                    $.each(data, function (key, comment) {
                        comments.push(comment);
                    });
                });
        };

    return {
        comments: comments,
        getComments: getComments
    }
};
