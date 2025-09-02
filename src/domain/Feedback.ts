export type Feedback = {
    userName: string;
    content: string;
    score: {
        cleanliness: number;
        service: number;
        speed: number;
        location: number;
        speech: number;
    }
}