export class EventData {
    constructor(
        public timestamp: number,
        public tag: string,
        public valor: string | null
    ) {        
    }
}