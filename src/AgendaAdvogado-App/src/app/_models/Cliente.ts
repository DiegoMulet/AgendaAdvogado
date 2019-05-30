import { Processo } from './Processo';

export class Cliente {
    constructor() {}

    id: number;
    nome: string;
    cnpj: number;
    estado: string;
    processos: Processo[];

}
