export class OperationResult {
    constructor(public readonly message: string, public readonly status: OperationResultStatus) { }
}

export enum OperationResultStatus {
    Success = 1,

    Failure = 2
}