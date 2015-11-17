class graphModel {
    constructor(modelStates) {
        this.modelStates = modelStates;
        this.current = modelStates[0];
    }
    current: string;
    modelStates: string[];
    nextClass: string = 'btn btn-primary';
    prevClass: string = 'disabled-btn';
    public index(): number {
        return this.modelStates.indexOf(this.current);
    }
    public nextState() {
        this.current === 'undefined' ? this.current = this.modelStates[0] : '';

        var i = this.modelStates.indexOf(this.current);
        if (!(typeof this.modelStates[i + 1] === 'undefined')) {
            this.current = this.modelStates[i + 1];
        }
    }
    
    public prevState() {
        this.current === 'undefined' ? this.current = this.modelStates[0] : '';
        var i = this.modelStates.indexOf(this.current);
        if (i > 0 && !(typeof this.modelStates[i - 1] === 'undefined')) {
            this.current = this.modelStates[i - 1];
        }
    }
    
}