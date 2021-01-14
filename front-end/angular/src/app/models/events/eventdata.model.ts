export class EventData {
    constructor(
        public timestamp: number,
        public tag: string,
        public valor: string | null,
        public status: number | null = null,
        public id?: number
    ) {        
    }
}