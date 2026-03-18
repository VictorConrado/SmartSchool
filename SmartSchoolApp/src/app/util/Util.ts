export class Util {
  static nomeConcat(item: any[]): string {

    if (!item) {
      return '';
    }

    return item.map(x => x.nome).join(',');
  }
}