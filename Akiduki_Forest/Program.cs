using System.ComponentModel;
using System.Reflection.Emit;
using System.Text;
using System.Xml.Linq;

bool playing = true;
//Null警告を出さないためにstring"?"としている。
string? command;

//セーブ用の区切り(節)
int section = 0;

//ステータス
int hp = 200;//体力
int mp = 30;//魔力
int lack = 0;//運

//アイテム
int item = 0;//アイテムの総重量
int item_bom = 0;//手榴弾の数(手榴弾の重さは*2で算出)
int item_bullet = 0;//弾丸の数
int item_cure = 0;//医療品の数

//誤ったコマンドを入力しているときはループ
while (playing == true)
{
    Console.WriteLine("1:はじめから");
    Console.WriteLine("2:途中から");
    Console.WriteLine("3:終了する");
    Console.Write("数値を入力してください:");
    command = Console.ReadLine();
    Console.WriteLine("");
    switch (command) 
    {
        case "1":
            playing = false;
            Console.WriteLine("「うーん…暇だなぁ」");
            Console.ReadLine();//次のコメントを表示(ReadKeyでもいいけど、ReadLineのほうが改行されて読みやすい気がするのでこちらに。)
            Console.WriteLine("彼女、秋月は暇をしていた。");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("数年前の大規模な戦争も今は終わり、平和な世の中となった今では傭兵としての仕事はなくなっていた。");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("ここ、アネリアの森は都市から離れた場所にあり、秋月が住んでいる家はその森の中にあった。");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("「とりあえず、外で散歩でもしようかなぁ…」");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("秋月はそう言って椅子から立ち上がった。");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("近くの机に置いている拳銃と、2本の小型ナイフを持ち、近くのリュックを背負った。");
            Console.ReadLine();//次のコメントを表示

            //初期状態
            item = 12;//アイテムの総重量
            item_bom = 0;//手榴弾の数(手榴弾の重さは*2で算出)
            item_bullet = 0;//弾丸の数
            item_cure = 0;//医療品の数

            //アイテム選択処理
            bool choose_item = true;
            while (choose_item == true) 
            {
                Console.WriteLine("");
                Console.WriteLine("その他に冒険に持っていくものを決めてください");
                Console.WriteLine("");
                Console.WriteLine("持っていけるもの(最大12)");
                Console.WriteLine("1:手榴弾(攻撃/爆破)[1つ当たりの重さ:2]");
                Console.WriteLine("2:弾薬(攻撃)[1セット(10発)当たりの重さ:1]");
                Console.WriteLine("3:医療品(回復)[1つ当たりの重さ:1]");
                Console.WriteLine("");

                bool choose_item_bom = true;
                while (choose_item_bom == true)
                {
                    Console.WriteLine("1:手榴弾 を何個持っていきますか?");
                    if (!int.TryParse(Console.ReadLine(), out item_bom))
                    {
                        Console.WriteLine("半角数値を入力してください");
                    }
                    else
                    {
                        if (item - item_bom * 2 >= 0) 
                        {
                            item -= item_bom * 2;
                            Console.WriteLine("{0}個(重さ{1}|残り重量{2})", item_bom, item_bom * 2, item);
                            choose_item_bom = false;
                        }
                        else
                        {
                            Console.WriteLine("数値が正しくありません。");
                        }
                    }
                }

                bool choose_item_bullet = true;
                while (choose_item_bullet == true)
                {
                    Console.WriteLine("2:弾薬 を何セット持っていきますか?");
                    if (!int.TryParse(Console.ReadLine(), out item_bullet))
                    {
                        Console.WriteLine("半角数値を入力してください");
                    }
                    else
                    {
                        if (item - item_bullet >= 0)
                        {
                            item -= item_bullet;
                            Console.WriteLine("{0}個(重さ{1}|残り重量{2})", item_bullet, item_bullet, item);
                            choose_item_bullet = false;
                        }
                        else
                        {
                            Console.WriteLine("数値が正しくありません。");
                        }
                    }
                }

                bool choose_item_cure = true;
                while (choose_item_cure == true)
                {
                    Console.WriteLine("3:医療品 を何個持っていきますか?");
                    if (!int.TryParse(Console.ReadLine(), out item_cure))
                    {
                        Console.WriteLine("半角数値を入力してください");
                    }
                    else
                    {
                        if (item - item_cure >= 0)
                        {
                            item -= item_cure;
                            Console.WriteLine("{0}個(重さ{1}|残り重量{2})", item_cure, item_cure, item);
                            choose_item_cure = false;
                        }
                        else
                        {
                            Console.WriteLine("数値が正しくありません。");
                        }
                    }
                }

                Console.WriteLine("手榴弾{0}個、弾丸{1}個、医療品{2}個でよろしいですか?)", item_bom, item_bullet, item_cure);
                bool choose_item_last = true;
                while (choose_item_last == true)
                {
                    Console.WriteLine("1:はい/2:いいえ");
                    command = Console.ReadLine();
                    if (command == "1")
                    {
                        choose_item_last = false;
                        choose_item = false;
                        //現時点では、弾丸のセット数を記録しているため、弾数表記に変更
                        item_bullet *= 10;
                        Console.WriteLine("");
                    }
                    else if (command == "2")
                    {
                        choose_item_last = false;
                        Console.WriteLine("もう一度再設定します。");
                        //アイテム数の初期化
                        item = 12;
                        item_bom = 0;
                        item_bullet = 0;
                        item_cure = 0;
                    }
                    else
                    {
                        Console.WriteLine("入力が正しくありません。");
                    }
                    Console.WriteLine("");
                }
            }

            //ロード地点(1)
            label1:
            section = 1;
            
            Console.WriteLine("移動中…");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("「ん～」");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("「外は涼しくて気持ちいいねぇ～」");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("「さて、今日はどこに行こうかなぁ」");
            Console.ReadLine();//次のコメントを表示

            bool where = false;
            while (where == false)
            {
                Console.WriteLine("");
                Console.WriteLine("どちらへ行きますか?");
                Console.WriteLine("1:秘密の森");
                Console.WriteLine("2:輝く湖畔");
                Console.WriteLine("3:怪しい影");
                Console.WriteLine("----------");
                Console.WriteLine("4:セーブ");

                command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                    where = true;
                        lack += 5;
                        Console.WriteLine("珍しい植物を見つけた！");
                        Console.ReadLine();//次のコメントを表示
                        break;

                    case "2":
                    where = true;
                        Console.WriteLine("特にめぼしい物は見当たらなかった。");
                        Console.ReadLine();//次のコメントを表示
                        break;

                    case "3":
                    where = true;
                        Console.WriteLine("怪しい影に近づいてみると、何かはわからないが、中型の生物のように見える。");
                        Console.ReadLine();//次のコメントを表示
                        Console.WriteLine("こちらに対して敵対的な視線を向けて、今すぐにでも攻撃してきそうだ。");
                        Console.ReadLine();//次のコメントを表示
                        lack -= 5;
                        //戦闘処理
                        Buttle();
                        //もし死んでいるなら終了する
                        if (section == 99)
                            goto label99;
                        break;

                    case "4":
                        //セーブ処理
                        Save();
                        break;

                    default:
                        Console.WriteLine("半角数字を入力してください。");
                        break;
                }
            }

            //ロード地点(2)
            label2:
            section = 2;

            Console.WriteLine("更に森の奥を進んでいく。");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("すると、山の斜面に石の建造物らしきものが見えた。");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("「あれは…何かしらね。」");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("ここまで遠くに来たのは今日が初めてだ。");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("また、こんな森の奥まで来る人はほとんどいないだろう。");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("「こんなところに…何の建物かしら？」");
            Console.ReadLine();//次のコメントを表示

            where = false;
            while (where == false)
            {
                Console.WriteLine("");
                Console.WriteLine("どうしますか");
                Console.WriteLine("1:すぐに建造物に近づく");
                Console.WriteLine("2:建造物の周りを調べる");
                Console.WriteLine("3:一旦木陰で休憩する");
                Console.WriteLine("----------");
                Console.WriteLine("4:セーブ");

                command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        where = true;
                        Console.WriteLine("見たことがない建物を見た秋月は、すぐにそこへ近づいた。");
                        Console.ReadLine();//次のコメントを表示
                        break;

                    case "2":
                        where = true;
                        Console.WriteLine("建造物の周辺を見渡すと、近くには紅の花と、鮮やかな秘色(ひそく)の花が咲き乱れていた。");
                        /*特に意味はないが、花の紹介(後付け設定)

                        紅の花:ゼラニウム(天竺葵-てんじくあおい)
                        4月~6月開花の四季咲き植物。北大陸は冬の寒さが厳しいので冬は散ってしまう。
                        花言葉:信頼 真の友情 尊敬 決心 | (外国)愚かさ 良い育ち 上流階級 | (赤色)君ありて幸福 あなたがいて幸せ
                        ハンガリーの国花。

                        秘色の花:ワスレナグサ(勿忘草)
                        3月下旬~4月に見られる。(北大陸は夏でもそれほど気温が上がらないため(もちろん地域による差はあるが)、夏設定の本作品でもつじつまは合う。)
                        花言葉:私を忘れないで 真実の愛*/
                        Console.ReadLine();//次のコメントを表示
                        Console.WriteLine("微かに香る花の香りで癒された。");
                        Console.ReadLine();//次のコメントを表示
                        lack += 3;
                        hp += 20;//微妙に回復(本作ではHPの最大値はない(しいて言うならint(32bit整数(負の数を含める))が最大値。))
                        Console.WriteLine("先ほどの建物ことをふと思い出した。");
                        Console.ReadLine();//次のコメントを表示
                        Console.WriteLine("こんな森の奥に誰が作ったのかが気になり、気が付くとその足はそちらへと動いていた。");
                        Console.ReadLine();//次のコメントを表示
                        break;

                    case "3":
                        where = true;
                        Console.WriteLine("一旦木陰で休憩することにした。");
                        Console.ReadLine();//次のコメントを表示
                        Console.WriteLine("森林ならではの爽やかな風が頬に当たり心地が良い。");
                        Console.ReadLine();//次のコメントを表示
                        Console.WriteLine("視界、音、匂い。それらすべての要素が私の気分をとても穏やかにさせた。");
                        Console.ReadLine();//次のコメントを表示
                        hp += 50;
                        mp += 10;
                        Console.WriteLine("先ほどの建物ことをふと思い出した。");
                        Console.ReadLine();//次のコメントを表示
                        Console.WriteLine("こんな森の奥に誰が作ったのかが気になり、気が付くとその足はそちらへと動いていた。");
                        Console.ReadLine();//次のコメントを表示
                        break;

                    case "4":
                        //セーブ処理
                        Save();
                        break;

                    default:
                        Console.WriteLine("半角数字を入力してください。");
                        break;
                }
            }

            //ロード地点(3)
            label3:
            section = 3;

            Console.WriteLine("石で建造されたトンネルのようなその建造物は、山の内部へ続いている。");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("トンネルへ入り奥へ進んでいくと、道は二股に別れていた。");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("道は、両方とも壁掛けのたいまつで照らされていて、");
            Console.WriteLine("右の道は細い道が、左の道は曲がりくねった道が続いていた。");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("「うーん…どっちに行こうかなぁ」");
            Console.ReadLine();//次のコメントを表示

            where = false;
            while (where == false)
            {
                Console.WriteLine("");
                Console.WriteLine("どちらに行きますか");
                Console.WriteLine("1:右の道");
                Console.WriteLine("2:左の道");
                Console.WriteLine("----------");
                Console.WriteLine("3:セーブ");

                command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        where = true;
                        Console.WriteLine("右の道へ進んでいくと、その道はだんだんと細くなってきた。");
                        Console.ReadLine();//次のコメントを表示
                        Console.WriteLine("だんだんと地下へと進んでいった。");
                        Console.ReadLine();//次のコメントを表示
                        Console.WriteLine("「さ、寒いわね…」");
                        Console.ReadLine();//次のコメントを表示
                        Console.WriteLine("夏とはいえ、この地域は地上でも涼しいため、地下では寒いほどになってきた。");
                        Console.ReadLine();//次のコメントを表示
                        break;

                    case "2":
                        where = true;
                        Console.WriteLine("道なりに進んでいくと、何かの影のようなものが見えた。");
                        Console.ReadLine();//次のコメントを表示
                        Console.WriteLine("「なにかしら？」");
                        Console.ReadLine();//次のコメントを表示
                        Console.WriteLine("隠れながらのぞいてみると、そこには異様な機影が見えた。");
                        Console.ReadLine();//次のコメントを表示
                        Console.WriteLine("こちらにはまだ気づいていないようだ。");
                        Console.ReadLine();//次のコメントを表示
                        
                        //戦闘or道を戻って分岐点の右側の道に行くかの選択肢
                        bool which = false;
                        while (which == false)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("どうしますか");
                            Console.WriteLine("1:敵と戦闘する");
                            Console.WriteLine("2:先ほどの分岐点まで戻る");

                            command = Console.ReadLine();
                            switch (command)
                            {
                                case "1":
                                    which = true;
                                    //戦闘処理
                                    Buttle();
                                    //もし死んでいるなら終了する
                                    if (section == 99)
                                        goto label99;
                                    break;

                                case "2":
                                    which = true;
                                    where = false;
                                    Console.WriteLine("先ほどの分岐点まで戻った。");
                                    Console.ReadLine();//次のコメントを表示
                                    break;

                                default:
                                    Console.WriteLine("半角数字を入力してください。");
                                    break;
                            }
                        }
                        break;

                    case "3":
                        //セーブ処理
                        Save();
                        break;

                    default:
                        Console.WriteLine("半角数字を入力してください。");
                        break;
                }
            }

            Console.WriteLine("広い空間に出たので、一旦周りを見渡すことにした。");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("右手前側には、水たまりのような場所がある。");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("正面側には何もなく、左側に道が続いているようだった。");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("左側の道は一本道となっていて、特に迷うこともなく進んでいく。");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("しばらく進んでいると、見慣れないものがあった。");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("「なに？これ…魔法陣かしら？」");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("床には星形の模様がつき、天井に向けて水色の半透明なオーラが浮かび上がっていた。");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("そのオーラを左手で触れようとする。");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("一瞬の間をおいて、視界は真っ白になった。");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("白い光はただ眩しいではなく、温かさが感じられた。");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("数秒経った後、だんだんとその光は消えていった。");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("しかし、その視界に映っていたのは、先ほど居た地下ではなく、レンガ造りの建物の中だった。");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("建物にはアーチ状の窓が適度につけられており、先ほどの地下とは打って変わって明るい様子だった。");
            Console.ReadLine();//次のコメントを表示
            Console.WriteLine("窓を覗いてみると、下の様子は霧で見えず、今いる場所はかなり高い階なのかもしれないと思った。");
            Console.ReadLine();//次のコメントを表示
            //天空世界
            //下の階に行くか、上の階に行くかで大きく√分岐

            break;

        //ロード
        case "2":
            playing = false;

            //新システム

            //ファイル読み込み
            XDocument file_read = XDocument.Load(@"./Data.xml");

            IEnumerable<XElement> datas = file_read.Elements("Data");
            foreach (XElement data_read in datas)
            {
                if (int.TryParse(data_read.Element("Section")?.Value, out int temp2))
                    section = temp2;
                if (int.TryParse(data_read.Element("HP")?.Value, out int temp4))
                    hp = temp4;
                if (int.TryParse(data_read.Element("MP")?.Value, out int temp5))
                    mp = temp5;
                if (int.TryParse(data_read.Element("Luck")?.Value, out int temp3))
                    lack = temp3;
                if (int.TryParse(data_read.Element("item")?.Value, out int temp_item1))
                    item = temp_item1;
                if (int.TryParse(data_read.Element("item_bom")?.Value, out int temp_item2))
                    item_bom = temp_item2;
                if (int.TryParse(data_read.Element("item_bullet")?.Value, out int temp_item3))
                    item_bullet = temp_item3;
                if (int.TryParse(data_read.Element("item_cure")?.Value, out int temp_item4))
                    item_cure = temp_item4;

                Console.WriteLine("ロードが成功しました。");
                Console.ReadLine();//次のコメントを表示
                switch (section)
                {
                    case 1:
                        goto label1;

                    case 2:
                        goto label2;

                    case 3:
                        goto label3;

                    default:
                        break;
                }
            }

            break;

        case "3":
            label99:
            playing = false;
            break;

        default:
            Console.WriteLine("誤ったコマンドを入力しています。");
            break;
    }
}

//セーブ処理
void Save()
{
    //データ保存(xml)
    XElement element = new
        (
            "Data",
            new XElement("Section", section),
            new XElement("HP", hp),
            new XElement("MP", mp),
            new XElement("Luck", lack),
            new XElement("item", item),
            new XElement("item_bom", item_bom),
            new XElement("item_bullet", item_bullet),
            new XElement("item_cure", item_cure)
        );
    element.Save("Data.xml");
    Console.WriteLine("セーブが完了しました。");
}

//戦闘処理
void Buttle() 
{
    Console.WriteLine(" -+-+- 戦闘開始 -+-+- ");
    Console.ReadLine();//次のコメントを表示
    
    //ランダムな数値用
    Random rand = new();

    //敵の体力(初期値):100~240(10刻み)
    int enemy_hp = 100 + rand.Next(0,15) * 10;
    //他のところから変更ができるように
    string enemy_name = "異形の存在";

    //難易度調節のために戦闘開始時自動回復
    hp += 20 + rand.Next(0,3) * 10;//20~40(10刻み)
    mp += rand.Next(2,6);//2~5

    while (enemy_hp > 0)
    {
        Console.WriteLine(" - あなたのターン - ");
        Console.ReadLine();//次のコメントを表示

        //キャラクターステータス表示
        Console.WriteLine("");
        Console.WriteLine("味方ステータス");
        Console.WriteLine("|秋月小鞠|HP:{0}|MP:{1}|", hp, mp);
        Console.WriteLine("");
        Console.WriteLine("敵ステータス");
        Console.WriteLine("|{0}|HP:{1}|", enemy_name, enemy_hp);

        //作成途中
        bool where = false;
        while (where == false)
        {

            Console.WriteLine("");
            Console.WriteLine("どのような行動をしますか");
            Console.WriteLine("1:ナイフ攻撃");
            Console.WriteLine("2:手榴弾攻撃(残り{0}個)", item_bom);
            Console.WriteLine("3:拳銃射撃(残り{0}発)", item_bullet);
            Console.WriteLine("4:治療魔法");//MPを使って回復
            Console.WriteLine("5:回復薬治療(残り{0}個)", item_cure);
            Console.WriteLine("6:戦略的撤退");//逃げる
            Console.WriteLine("7:手榴弾退散(残り{0}個)", item_bom);//手榴弾の爆破と同時に逃げることで「戦略的撤退」よりも高確率で逃げ切れる
            Console.WriteLine("8:敵味方のステータスを再確認");

            command = Console.ReadLine();
            switch (command)
            {
                case "1":
                    where = true;
                    //近接戦闘
                    Console.WriteLine("敵に接近して、ナイフを振り上げて…");
                    Console.WriteLine("その腕を素早く振り降ろした。");
                    Console.ReadLine();//次のコメントを表示
                    Console.WriteLine("敵はダメージを受けたようだ。");
                    Console.ReadLine();//次のコメントを表示
                    enemy_hp -= 50;//固定ダメージ
                    break;

                case "2":
                    //爆破攻撃
                    if (item_bom > 0)
                    {
                        where = true;
                        item_bom -= 1;
                        Console.WriteLine("秋月は後ろずさりで少しずつ後退しながら、手榴弾のピンを抜いて、");
                        Console.WriteLine("敵のほうに向かって投げつけた。");
                        Console.ReadLine();//次のコメントを表示
                        Console.WriteLine("しばらくすると、大きな爆発音を上げ、トンネルの中は白い煙に包まれた。");
                        Console.ReadLine();//次のコメントを表示
                        enemy_hp -= 150 + rand.Next(0, 4) * 10;//150~180(10刻み)
                    }
                    else
                    {
                        Console.WriteLine("爆弾がないようだ。");
                        Console.ReadLine();//次のコメントを表示
                    }
                    break;
                    
                case "3":
                    //射撃攻撃処理
                    //(ナイフ/爆弾との違いのために命中率は低く、ダメージが大きい、貫通するとさらにダメージ増加とかのほうがいいかも。
                    //↑一応、弾薬数が多いため攻撃可能回数がほぼ無限というメリットもあるけど。)
                    if (item_bullet > 0)
                    {
                        where = true;
                        item_bullet -= 1;
                        Console.WriteLine("拳銃に弾丸を込め、単発射撃を行った。");
                        Console.ReadLine();//次のコメントを表示
                        Console.WriteLine("拳銃は、爆音を上げて、強い反動を受けた。");
                        Console.ReadLine();//次のコメントを表示
                        Console.WriteLine("弾丸は敵を直撃して、敵の体を貫通した。");
                        Console.ReadLine();//次のコメントを表示
                        enemy_hp -= 40 + rand.Next(0, 27) * 10;// 40~300(10刻み)
                    }
                    else
                    {
                        Console.WriteLine("弾薬が足りないようだ。");
                        Console.ReadLine();//次のコメントを表示
                    }
                    break;

                case "4":
                    //治療処理
                    if (mp >= 3)
                    {
                        where = true;
                        mp -= 3;
                        Console.WriteLine("「リヴァレド・ハート」");
                        Console.WriteLine("両手を空にかざし、回復魔法を唱えた。");
                        Console.ReadLine();//次のコメントを表示
                        Console.WriteLine("優しいオーラが秋月の体を包み込む。");
                        Console.ReadLine();//次のコメントを表示
                        Console.WriteLine("傷は見る見るうちに回復していった。");
                        Console.ReadLine();//次のコメントを表示
                        hp += 160;//固定
                    }
                    else
                    {
                        where = true;
                        Console.WriteLine("魔力が足りないため、簡易的な治療のみ行うことにした。");
                        Console.ReadLine();//次のコメントを表示
                        Console.WriteLine("ナイフでスカートの先を切り、その布で傷ついた部分を保護した。");
                        Console.ReadLine();//次のコメントを表示
                        hp += 50;//固定
                    }

                    break;

                case "5":
                    //アイテム治療処理
                    if (item_cure > 0)
                    {
                        where = true;
                        item_cure -= 1;
                        Console.WriteLine("医療品をあさり、治療に使えそうな薬品などを取り出した。");
                        Console.ReadLine();//次のコメントを表示
                        Console.WriteLine("傷ついた部分に治療薬を塗り、包帯で巻いて痛みを和らげることができた。");
                        Console.ReadLine();//次のコメントを表示
                        hp += 200;//固定
                    }
                    else
                    {
                        Console.WriteLine("治療薬がないようだ。");
                        Console.ReadLine();//次のコメントを表示
                    }
                    break;

                case "6":
                    //確立で逃げる処理(50%の確率で逃げられる)
                    if (rand.Next(0, 101) >= 50)
                    {
                        Console.WriteLine("敵からなんとか逃げ切ることができた。");
                        Console.ReadLine();//次のコメントを表示
                        enemy_hp = 0;
                    }
                    else
                    {
                        Console.WriteLine("敵に回り込まれた。");
                        Console.ReadLine();//次のコメントを表示
                    }

                    break;

                case "7":
                    //逃げる処理(必ず)
                    enemy_hp = 0;
                    break;

                case "8":
                    Console.WriteLine("");
                    Console.WriteLine("味方ステータス");
                    Console.WriteLine("|秋月小鞠|HP:{0}|MP:{1}|", hp, mp);
                    Console.WriteLine("");
                    Console.WriteLine("敵ステータス");
                    Console.WriteLine("|{0}|HP:{1}|", enemy_name, enemy_hp);
                    break;

                default:
                    Console.WriteLine("半角数字を入力してください。");
                    break;
            }

            //敵死亡判定
            if (enemy_hp <= 0)
            {
                Console.WriteLine("敵はその場に倒れ、もう二度と動かなくなった。");
                Console.ReadLine();//次のコメントを表示
                Console.WriteLine(" -+-+- 戦闘終了 -+-+- ");
                Console.ReadLine();//次のコメントを表示
                Console.WriteLine("「ふう。何とかなったわね。」");
                Console.ReadLine();//次のコメントを表示
                Console.WriteLine("「先へ進むわ。」");
                Console.ReadLine();//次のコメントを表示
            }
            else
            {
                //続行(敵ターン)
                Console.WriteLine(" - 敵のターン - ");
                Console.ReadLine();//次のコメントを表示

                if (rand.Next(0, 101) <= 70)//70%の確率
                {
                    Console.WriteLine("敵からの攻撃");
                    Console.ReadLine();//次のコメントを表示
                    hp -= rand.Next(5, 14) * 10;//50~130(10刻み)(最大値が出るとまあまあ強いのでHPに注意。)
                }
                else if (rand.Next(0, 101) <= 50)//全体で15%の確率(残り30%のうち、更に半分の確率)
                {
                    Console.WriteLine("敵は様子を見ている。");
                    Console.ReadLine();//次のコメントを表示
                }
                else//全体で15%
                {
                    Console.WriteLine("敵は回復魔法を唱えた。");
                    Console.ReadLine();//次のコメントを表示
                    enemy_hp += 120;//固定
                }

                //秋月死亡判定
                if (hp <= 0)
                {
                    Console.WriteLine("");
                    Console.WriteLine("「う、うそ…」");
                    Console.ReadLine();//次のコメントを表示
                    Console.WriteLine("体に大きな傷を負った秋月はもう、そこから動くことができなくなってしまった。");
                    Console.ReadLine();//次のコメントを表示
                    Console.WriteLine("視界に広がる真紅の液体。それが自分の運命を示唆しているようだった。");
                    Console.ReadLine();//次のコメントを表示
                    Console.WriteLine("「こ…こんなところで死ぬはずは…」");
                    Console.ReadLine();//次のコメントを表示
                    Console.WriteLine("言葉を言い終える前に、その人生の終わりを早々と迎えた。");
                    Console.ReadLine();//次のコメントを表示
                    //終了判定
                    enemy_hp = 0;
                    section = 99;
                }
            }
        }
    }
}
