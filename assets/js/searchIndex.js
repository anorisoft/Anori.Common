
var camelCaseTokenizer = function (builder) {

  var pipelineFunction = function (token) {
    var previous = '';
    // split camelCaseString to on each word and combined words
    // e.g. camelCaseTokenizer -> ['camel', 'case', 'camelcase', 'tokenizer', 'camelcasetokenizer']
    var tokenStrings = token.toString().trim().split(/[\s\-]+|(?=[A-Z])/).reduce(function(acc, cur) {
      var current = cur.toLowerCase();
      if (acc.length === 0) {
        previous = current;
        return acc.concat(current);
      }
      previous = previous.concat(current);
      return acc.concat([current, previous]);
    }, []);

    // return token for each string
    // will copy any metadata on input token
    return tokenStrings.map(function(tokenString) {
      return token.clone(function(str) {
        return tokenString;
      })
    });
  }

  lunr.Pipeline.registerFunction(pipelineFunction, 'camelCaseTokenizer')

  builder.pipeline.before(lunr.stemmer, pipelineFunction)
}
var searchModule = function() {
    var documents = [];
    var idMap = [];
    function a(a,b) { 
        documents.push(a);
        idMap.push(b); 
    }

    a(
        {
            id:0,
            title:"IActivatable",
            content:"IActivatable",
            description:'',
            tags:''
        },
        {
            url:'/Anori.Common/api/Anori.Common/IActivatable',
            title:"IActivatable",
            description:""
        }
    );
    a(
        {
            id:1,
            title:"IActivated",
            content:"IActivated",
            description:'',
            tags:''
        },
        {
            url:'/Anori.Common/api/Anori.Common/IActivated',
            title:"IActivated",
            description:""
        }
    );
    a(
        {
            id:2,
            title:"ActivatableObservableCollection",
            content:"ActivatableObservableCollection",
            description:'',
            tags:''
        },
        {
            url:'/Anori.Common/api/Anori.Common/ActivatableObservableCollection',
            title:"ActivatableObservableCollection",
            description:""
        }
    );
    a(
        {
            id:3,
            title:"ActivatableObservableCollection",
            content:"ActivatableObservableCollection",
            description:'',
            tags:''
        },
        {
            url:'/Anori.Common/api/Anori.Common/ActivatableObservableCollection_1',
            title:"ActivatableObservableCollection<T>",
            description:""
        }
    );
    a(
        {
            id:4,
            title:"IActivatable",
            content:"IActivatable",
            description:'',
            tags:''
        },
        {
            url:'/Anori.Common/api/Anori.Common/IActivatable_1',
            title:"IActivatable<TSelf>",
            description:""
        }
    );
    a(
        {
            id:5,
            title:"EventArgs",
            content:"EventArgs",
            description:'',
            tags:''
        },
        {
            url:'/Anori.Common/api/Anori.Common/EventArgs_1',
            title:"EventArgs<T>",
            description:""
        }
    );
    a(
        {
            id:6,
            title:"ResetLazy",
            content:"ResetLazy",
            description:'',
            tags:''
        },
        {
            url:'/Anori.Common/api/Anori.Common/ResetLazy_1',
            title:"ResetLazy<T>",
            description:""
        }
    );
    a(
        {
            id:7,
            title:"LazyThreadSafetyMode",
            content:"LazyThreadSafetyMode",
            description:'',
            tags:''
        },
        {
            url:'/Anori.Common/api/Anori.Common/LazyThreadSafetyMode',
            title:"LazyThreadSafetyMode",
            description:""
        }
    );
    var idx = lunr(function() {
        this.field('title');
        this.field('content');
        this.field('description');
        this.field('tags');
        this.ref('id');
        this.use(camelCaseTokenizer);

        this.pipeline.remove(lunr.stopWordFilter);
        this.pipeline.remove(lunr.stemmer);
        documents.forEach(function (doc) { this.add(doc) }, this)
    });

    return {
        search: function(q) {
            return idx.search(q).map(function(i) {
                return idMap[i.ref];
            });
        }
    };
}();
