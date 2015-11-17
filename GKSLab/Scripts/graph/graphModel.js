var graphModel = (function () {
    function graphModel(modelStates) {
        this.nextClass = 'btn btn-primary';
        this.prevClass = 'disabled-btn';
        this.modelStates = modelStates;
        this.current = modelStates[0];
    }
    graphModel.prototype.index = function () {
        return this.modelStates.indexOf(this.current);
    };
    graphModel.prototype.nextState = function () {
        this.current === 'undefined' ? this.current = this.modelStates[0] : '';
        var i = this.modelStates.indexOf(this.current);
        if (!(typeof this.modelStates[i + 1] === 'undefined')) {
            this.current = this.modelStates[i + 1];
        }
    };
    graphModel.prototype.prevState = function () {
        this.current === 'undefined' ? this.current = this.modelStates[0] : '';
        var i = this.modelStates.indexOf(this.current);
        if (i > 0 && !(typeof this.modelStates[i - 1] === 'undefined')) {
            this.current = this.modelStates[i - 1];
        }
    };
    return graphModel;
})();
