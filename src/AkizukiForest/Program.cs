using System.Xml.Linq;

bool playing = true;    // ループ用
int section = 0;        // セーブ用
string? command;        // コマンド読み取り

// ステータス
int komariHP = 200;     // 体力
int komariMP = 30;      // 魔力
int komariLack = 0;     // 運

// アイテム
int item = 12;          // アイテムの総重量
int itemBom = 0;        // 手榴弾の数 (手榴弾の重さはx2で算出)
int itemBullet = 0;     // 弾丸の数
int itemMedicine = 0;   // 医療品の数

// 敵のステータス
int enemyHP = 0;        // 体力
string enemyName = "";  // 名称

// 誤ったコマンドを入力しているときはループ
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
            Console.ReadLine(); // 次のコメントを表示 (ReadKey でもいいけど、ReadLine のほうが改行されて読みやすい気がするのでこちらに)
            Console.WriteLine("彼女、秋月は暇をしていた。");
            Console.ReadLine();
            Console.WriteLine("数年前の大規模な戦争も今は終わり、平和な世の中となった今では傭兵としての仕事はなくなっていた。");
            Console.ReadLine();
            Console.WriteLine("ここ、アネリアの森は都市から離れた場所にあり、秋月が住んでいる家はその森の中にあった。");
            Console.ReadLine();
            Console.WriteLine("「とりあえず、外で散歩でもしようかなぁ…」");
            Console.ReadLine();
            Console.WriteLine("秋月はそう言って椅子から立ち上がった。");
            Console.ReadLine();
            Console.WriteLine("近くの机に置いている拳銃と、2本の小型ナイフを持ち、近くのリュックを背負った。");
            Console.ReadLine();

            // アイテム選択処理
            bool selectingItem = true;
            while (selectingItem == true)
            {
                Console.WriteLine("");
                Console.WriteLine("その他に冒険に持っていくものを決めてください");
                Console.WriteLine("");
                Console.WriteLine("持っていけるもの(最大12)");
                Console.WriteLine("1:手榴弾(攻撃/爆破)[1つ当たりの重さ:2]");
                Console.WriteLine("2:弾薬(攻撃)[1セット(10発)当たりの重さ:1]");
                Console.WriteLine("3:医療品(回復)[1つ当たりの重さ:1]");
                Console.WriteLine("");

                ItemSelection("1:手榴弾", ref item, ref itemBom, 2);
                ItemSelection("2:弾薬", ref item, ref itemBullet, 1);
                ItemSelection("3:医療品", ref item, ref itemMedicine, 1);

                void ItemSelection(string _itemName, ref int _item, ref int _itemEach, int _itemWeight)
                {
                    bool selectingEachItems = true;
                    while (selectingEachItems == true)
                    {
                        Console.WriteLine(_itemName + " を何個持っていきますか?");
                        if (int.TryParse(Console.ReadLine(), out _itemEach) && (_item - _itemEach * _itemWeight >= 0))
                        {
                            _item -= _itemEach * _itemWeight;
                            Console.WriteLine("{0}個(重さ{1}|残り重量{2})", _itemEach, _itemEach * _itemWeight, _item);
                            selectingEachItems = false;
                        }
                        else
                            InvalidInput();
                    }
                }

                Console.WriteLine("手榴弾{0}個、弾丸{1}個、医療品{2}個でよろしいですか?)", itemBom, itemBullet, itemMedicine);
                bool checkingItems = true;
                while (checkingItems == true)
                {
                    Console.WriteLine("1:はい/2:いいえ");
                    command = Console.ReadLine();
                    if (command == "1")
                    {
                        checkingItems = false;
                        selectingItem = false;
                        // 現時点では、弾丸のセット数を記録しているため、弾数表記に変更
                        itemBullet *= 10;
                        Console.WriteLine("");
                    }
                    else if (command == "2")
                    {
                        checkingItems = false;
                        Console.WriteLine("もう一度再設定します。");
                        // アイテム数の初期化
                        item = 12;
                        itemBom = 0;
                        itemBullet = 0;
                        itemMedicine = 0;
                    }
                    else
                        InvalidInput();
                    Console.WriteLine("");
                }
            }

        //ロード地点(1)
        label1:
            section = 1;

            Console.WriteLine("移動中…");
            Console.ReadLine();
            Console.WriteLine("「ん～」");
            Console.ReadLine();
            Console.WriteLine("「外は涼しくて気持ちいいねぇ～」");
            Console.ReadLine();
            Console.WriteLine("「さて、今日はどこに行こうかなぁ」");
            Console.ReadLine();

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
                        komariLack += 5;
                        Console.WriteLine("珍しい植物を見つけた！");
                        Console.ReadLine();
                        break;

                    case "2":
                        where = true;
                        Console.WriteLine("特にめぼしい物は見当たらなかった。");
                        Console.ReadLine();
                        break;

                    case "3":
                        where = true;
                        Console.WriteLine("怪しい影に近づいてみると、何かはわからないが、中型の生物のように見える。");
                        Console.ReadLine();
                        Console.WriteLine("こちらに対して敵対的な視線を向けて、今すぐにでも攻撃してきそうだ。");
                        Console.ReadLine();
                        komariLack -= 5;
                        // 戦闘処理
                        Buttle();
                        break;

                    case "4":
                        Save();
                        break;

                    default:
                        InvalidInput();
                        break;
                }
            }

        //ロード地点(2)
        label2:
            section = 2;

            Console.WriteLine("更に森の奥を進んでいく。");
            Console.ReadLine();
            Console.WriteLine("すると、山の斜面に石の建造物らしきものが見えた。");
            Console.ReadLine();
            Console.WriteLine("「あれは…何かしらね。」");
            Console.ReadLine();
            Console.WriteLine("ここまで遠くに来たのは今日が初めてだ。");
            Console.ReadLine();
            Console.WriteLine("また、こんな森の奥まで来る人はほとんどいないだろう。");
            Console.ReadLine();
            Console.WriteLine("「こんなところに…何の建物かしら？」");
            Console.ReadLine();

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
                        Console.ReadLine();
                        break;

                    case "2":
                        where = true;
                        Console.WriteLine("建造物の周辺を見渡すと、近くには紅の花と、鮮やかな秘色(ひそく)の花が咲き乱れていた。");
                        // 特に意味はないが、花の紹介 (後付け設定)

                        // 紅の花: ゼラニウム (天竺葵-てんじくあおい)
                        // 4月~6月開花の四季咲き植物。北大陸は冬の寒さが厳しいので冬は散ってしまう。
                        // 花言葉: 信頼 真の友情 尊敬 決心 | (外国) 愚かさ 良い育ち 上流階級 | (赤色) 君ありて幸福 あなたがいて幸せ
                        // ハンガリーの国花。

                        // 秘色の花: ワスレナグサ (勿忘草)
                        // 3月下旬~4月に見られる。(北大陸は夏でもそれほど気温が上がらないため (もちろん地域による差はあるが)、夏設定の本作品でもつじつまは合う。)
                        // 花言葉: 私を忘れないで 真実の愛
                        Console.ReadLine();
                        Console.WriteLine("微かに香る花の香りで癒された。");
                        Console.ReadLine();
                        komariLack += 3;
                        komariHP += 20; // 微妙に回復 (本作では HP の最大値はない (しいて言うなら int 32bit が最大値))
                        Console.WriteLine("先ほどの建物ことをふと思い出した。");
                        Console.ReadLine();
                        Console.WriteLine("こんな森の奥に誰が作ったのかが気になり、気が付くとその足はそちらへと動いていた。");
                        Console.ReadLine();
                        break;

                    case "3":
                        where = true;
                        Console.WriteLine("一旦木陰で休憩することにした。");
                        Console.ReadLine();
                        Console.WriteLine("森林ならではの爽やかな風が頬に当たり心地が良い。");
                        Console.ReadLine();
                        Console.WriteLine("視界、音、匂い。それらすべての要素が私の気分をとても穏やかにさせた。");
                        Console.ReadLine();
                        komariHP += 50;
                        komariMP += 10;
                        Console.WriteLine("先ほどの建物ことをふと思い出した。");
                        Console.ReadLine();
                        Console.WriteLine("こんな森の奥に誰が作ったのかが気になり、気が付くとその足はそちらへと動いていた。");
                        Console.ReadLine();
                        break;

                    case "4":
                        Save();
                        break;

                    default:
                        InvalidInput();
                        break;
                }
            }

        //ロード地点(3)
        label3:
            section = 3;

            Console.WriteLine("石で建造されたトンネルのようなその建造物は、山の内部へ続いている。");
            Console.ReadLine();
            Console.WriteLine("トンネルへ入り奥へ進んでいくと、道は二股に別れていた。");
            Console.ReadLine();
            Console.WriteLine("道は、両方とも壁掛けのたいまつで照らされていて、");
            Console.WriteLine("右の道は細い道が、左の道は曲がりくねった道が続いていた。");
            Console.ReadLine();
            Console.WriteLine("「うーん…どっちに行こうかなぁ」");
            Console.ReadLine();

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
                        Console.ReadLine();
                        Console.WriteLine("だんだんと地下へと進んでいった。");
                        Console.ReadLine();
                        Console.WriteLine("「さ、寒いわね…」");
                        Console.ReadLine();
                        Console.WriteLine("夏とはいえ、この地域は地上でも涼しいため、地下では寒いほどになってきた。");
                        Console.ReadLine();
                        break;

                    case "2":
                        where = true;
                        Console.WriteLine("道なりに進んでいくと、何かの影のようなものが見えた。");
                        Console.ReadLine();
                        Console.WriteLine("「なにかしら？」");
                        Console.ReadLine();
                        Console.WriteLine("隠れながらのぞいてみると、そこには異様な機影が見えた。");
                        Console.ReadLine();
                        Console.WriteLine("こちらにはまだ気づいていないようだ。");
                        Console.ReadLine();

                        // 戦闘or道を戻って分岐点の右側の道に行くかの選択肢
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
                                    break;

                                case "2":
                                    which = true;
                                    where = false;
                                    Console.WriteLine("先ほどの分岐点まで戻った。");
                                    Console.ReadLine();
                                    break;

                                default:
                                    InvalidInput();
                                    break;
                            }
                        }
                        break;

                    case "3":
                        Save();
                        break;

                    default:
                        InvalidInput();
                        break;
                }
            }

            Console.WriteLine("広い空間に出たので、一旦周りを見渡すことにした。");
            Console.ReadLine();
            Console.WriteLine("右手前側には、水たまりのような場所がある。");
            Console.ReadLine();
            Console.WriteLine("正面側には何もなく、左側に道が続いているようだった。");
            Console.ReadLine();
            Console.WriteLine("左側の道は一本道となっていて、特に迷うこともなく進んでいく。");
            Console.ReadLine();
            Console.WriteLine("しばらく進んでいると、見慣れないものがあった。");
            Console.ReadLine();
            Console.WriteLine("「なに？これ…魔法陣かしら？」");
            Console.ReadLine();
            Console.WriteLine("床には星形の模様がつき、天井に向けて水色の半透明なオーラが浮かび上がっていた。");
            Console.ReadLine();
            Console.WriteLine("そのオーラを左手で触れようとする。");
            Console.ReadLine();
            Console.WriteLine("一瞬の間をおいて、視界は真っ白になった。");
            Console.ReadLine();
            Console.WriteLine("白い光はただ眩しいではなく、温かさが感じられた。");
            Console.ReadLine();
            Console.WriteLine("数秒経った後、だんだんとその光は消えていった。");
            Console.ReadLine();
            Console.WriteLine("しかし、その視界に映っていたのは、先ほど居た地下ではなく、レンガ造りの建物の中だった。");
            // 天空世界
            Console.ReadLine();
            Console.WriteLine("建物にはアーチ状の窓が適度につけられており、先ほどの地下とは打って変わって明るい様子だった。");
            Console.ReadLine();
            Console.WriteLine("窓を覗いてみると、下の様子は霧で見えず、今いる場所はかなり高い階なのだろうと思った。");
            Console.ReadLine();
            Console.WriteLine("窓の外に気を取られていると、ふと後ろのほうに気配を感じた。");
            Console.ReadLine();
            Console.WriteLine("急いで振り返ると、そこには身長の高い怪物がいた。");
            Console.ReadLine();
            Console.WriteLine("急いで、近くの柱へ隠れ、そこから銃撃を開始することにした。");
            Console.ReadLine();
            // 強制戦闘のため、自動回復
            komariHP = (komariHP >= 260) ? komariHP : 260;
            komariMP = (komariMP >= 30) ? komariMP : 30;
            // 戦闘開始
            Buttle();

        // ロード地点 (4c)
        label4c:
            section = 6;

            Console.WriteLine("「はぁ、はぁ。」");
            Console.ReadLine();
            Console.WriteLine("「急に現れて、本当にびっくりしたわ。」");
            Console.ReadLine();
            Console.WriteLine("敵が現れた方向を見てみると、そこには階段が続いていた。");
            Console.ReadLine();
            Console.WriteLine("また新たな敵が来ないか、注意深く近づいていくと、階段は上下に続いているようだった。");
            Console.ReadLine();
            Console.WriteLine("「ここ、本当に何の場所なのかしらね…」");
            Console.ReadLine();
            Console.WriteLine("「さて、上か下か。」");
            Console.ReadLine();
            // 下の階に行くか、上の階に行くかで大きく√分岐
            // 天空世界の頂上到達√…天空世界の結末(たまたま地下洞窟と天空世界でつながってしまったことなど)を明かす
            // 天空世界の地上到達√…地下空洞の過去を明かす
            // 秘密コマンドにて窓から飛び降りる→森の泉(隠し)√…次回作や尾花と桜(北大陸)の世界観についてのフラグ・ヒント/秋月の過去について
            where = false;
            while (where == false)
            {
                Console.WriteLine("");
                Console.WriteLine("どうしますか");
                Console.WriteLine("1:上の階へ行く");
                Console.WriteLine("2:下の階へ行く");
                // 3:窓から飛び降りる (隠し√)
                // 3番を隠すことでフラグにしている。
                // (隠し√だけど、3番が飛ばされているというのは違和感があるし、結構見つけやすい√だと思う。)
                // (3番を飛ばしているのはただのミスだと思われるかもしれないけど)
                Console.WriteLine("----------");
                Console.WriteLine("4:セーブ");

                command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        where = true;
                        // 天空ルート(戦闘(ボス戦)後に神が現れて現世に戻らせてくれる)
                        Console.WriteLine("上の階へと進んでいくが、同じような風景が続いている。");
                        Console.ReadLine();
                        Console.WriteLine("全ての階が同じ内装で、家具や物がなく、ループしているように思える。");
                        Console.ReadLine();
                        Console.WriteLine("20階以上は上がっただろうか。");
                        Console.ReadLine();
                        Console.WriteLine("ここが最上階のようで、部屋は今までの階よりもかなり小さく、扉があるようだった。");
                        Console.ReadLine();
                        Console.WriteLine("扉を開けるとそこは屋上になっており、広い場所と1体の黒い奇妙な姿が、上には明るい空と白い雲が広がっていた。");
                        Console.ReadLine();
                        Console.WriteLine("向こうの黒い奇妙な姿は、こちらを確認すると同時に、一気に近づいてきた。");
                        Console.ReadLine();
                        // ボス戦
                        enemyHP = 1000;
                        enemyName = "黒色の影";
                        // (秋月回復)
                        komariHP = (komariHP >= 300) ? komariHP : 300;
                        komariHP += 240;
                        komariMP += 10;
                        // 戦闘開始
                        Buttle();

                        Console.WriteLine("「はぁ、はぁ…」");
                        Console.ReadLine();
                        Console.WriteLine("「なんなのよ…こいつ。結構強いじゃない…」");
                        Console.ReadLine();
                        Console.WriteLine("何とか倒せたもののかなりけがをしてしまった。");
                        Console.ReadLine();
                        Console.WriteLine("屋上のフェンスへと近づき、周辺を見回してみると、そこには絶景が広がっていた。");
                        Console.ReadLine();
                        Console.WriteLine("雲海が広がり、ところどころ青色の山が顔をのぞかせている。");
                        Console.ReadLine();
                        Console.WriteLine("雲の切れ目からは草原や色彩豊かな花畑が広がる。");
                        Console.ReadLine();
                        komariLack += 5;
                        Console.WriteLine("風景に見とれていると、後ろから気配を感じるようになった。");
                        Console.ReadLine();
                        Console.WriteLine("急いで拳銃を手に取り、後ろを振り返ってみると…");
                        Console.ReadLine();
                        Console.WriteLine("そこには長髪の神々しい人の姿があった。");
                        Console.ReadLine();
                        Console.WriteLine("「あの、敵ではないわ…」");
                        Console.ReadLine();
                        Console.WriteLine("彼女はそうつぶやいた。");
                        Console.ReadLine();
                        Console.WriteLine("「私は秋月小鞠。あなたは？」");
                        // 人の名前を聞く前に、自分の名前を言うという礼儀を通している秋月。
                        Console.ReadLine();
                        Console.WriteLine("銃は持ったままそのように質問をした。");
                        Console.ReadLine();
                        Console.WriteLine("すると、");
                        Console.ReadLine();
                        Console.WriteLine("「ええと…名前はないかしらね。」");
                        Console.ReadLine();
                        Console.WriteLine("といった。続けて、");
                        Console.ReadLine();
                        Console.WriteLine("「出身はどこかしら？」");
                        Console.ReadLine();
                        Console.WriteLine("彼女は私に対してそう投げかける。");
                        Console.ReadLine();
                        Console.WriteLine("「どこ出身に見える？」");
                        Console.ReadLine();
                        Console.WriteLine("わざとそういうように訊ねてみた");
                        Console.ReadLine();
                        Console.WriteLine("「ええと、地上のほうよね…」");
                        Console.ReadLine();
                        Console.WriteLine("その回答に疑問を覚えた。");
                        Console.ReadLine();
                        Console.WriteLine("「あの、ここはどこの国なのかしら…？」");
                        Console.ReadLine();
                        Console.WriteLine("そう聞いてみた。すると、すぐにこのように返された。");
                        Console.ReadLine();
                        Console.WriteLine("「ここは、天界。あなたの知っている場所ではないわ。」");
                        Console.ReadLine();
                        Console.WriteLine("天界…昔聞いたことがあるような気がした。とても平和で、景色の綺麗な場所があると…");
                        Console.ReadLine();
                        Console.WriteLine("「あの…元の世界には戻れるの…？」");
                        Console.ReadLine();
                        Console.WriteLine("不安になって聞いてみる。");
                        Console.ReadLine();
                        Console.WriteLine("彼女は困ったような顔をした。");
                        Console.ReadLine();
                        Console.WriteLine("「え…戻れないの？」");
                        Console.ReadLine();
                        Console.WriteLine("と聞くと、すぐに彼女は笑顔になり、口を開いた。");
                        Console.ReadLine();
                        Console.WriteLine("「冗談よ。すぐに戻れる。」");
                        Console.ReadLine();
                        Console.WriteLine("「だ、だよね…」");
                        Console.ReadLine();
                        Console.WriteLine("「少し待っててね。」");
                        Console.ReadLine();
                        Console.WriteLine("その言葉の次には、視界が暖かな白い光で覆われた。");
                        Console.ReadLine();
                        Console.WriteLine("ほんの僅かの時間の後、視界を支配する白い光はだんだんと薄くなっていき、やがて緑色が見えた。");
                        Console.ReadLine();
                        Console.WriteLine("鬱蒼と茂る白樺の森林、この季節ならではの涼しさをまとったこの空間はまぎれもなく私の知る森だった。");
                        Console.ReadLine();
                        Console.WriteLine("「本当にごめんなさいね。」");
                        Console.ReadLine();
                        Console.WriteLine("声の聞こえる後ろを見てみると、彼女の姿があった。");
                        Console.ReadLine();
                        Console.WriteLine("「こちらの手違いで地上と天界がつながってしまったらしくて…」");
                        Console.ReadLine();
                        Console.WriteLine("「あの洞窟には何もなかったのね…」");
                        // 何か歴史的な遺産やお宝があると期待して洞窟に入ったが、秋月にとってはどうでもよい天界のワープゲートしかなかったということ。
                        Console.ReadLine();
                        Console.WriteLine("「本当に申し訳ないわ…」");
                        Console.ReadLine();
                        Console.WriteLine("「別にいいわ…暇つぶしになったし。」");
                        Console.ReadLine();
                        Console.WriteLine("そういうと、彼女は少し上を向いて次の言葉を言った。");
                        Console.ReadLine();
                        Console.WriteLine("「天界のほうで、黒い化け物がいたでしょう…」");
                        Console.ReadLine();
                        Console.WriteLine("「そうね。」");
                        Console.ReadLine();
                        Console.WriteLine("「天界の平和が脅かされるような事態になっていて…」");
                        Console.ReadLine();
                        Console.WriteLine("「大変そうね。」");
                        Console.ReadLine();
                        Console.WriteLine("「まあ、あなたには関係ないことよね…そろそろ別れましょうかね…」");
                        Console.ReadLine();
                        Console.WriteLine("「それでは。じゃあね。」");
                        Console.ReadLine();
                        Console.WriteLine("「ええ。さようなら。」");
                        Console.ReadLine();
                        Console.WriteLine("そういうと、彼女はだんだんと透明になり、消えていった。");
                        Console.ReadLine();
                        Console.WriteLine("非現実的な世界に思えたが、先ほどあったことを説明するには現実だと受け入れざるを得ない。");
                        Console.ReadLine();
                        Console.WriteLine("「奇妙なこともあるもんだね…」");
                        Console.ReadLine();
                        Console.WriteLine("先ほど彼女がいた方向に背を向け、歩き始めた。");
                        Console.ReadLine();
                        Console.WriteLine("");
                        Console.WriteLine("END:天界の青空(通常ルート1)");
                        Console.WriteLine("[天界の運命]");
                        Console.WriteLine("");
                        Console.ReadLine();
                        break;

                    case "2":
                        where = true;
                        // 地上ルート(地下の場所に関する文献を読める。また、魔導書かワープ装置か何かで現世に戻る。)
                        Console.WriteLine("下の階へ進んでいくと、窓からだんだんと地上の様子が見えてきた。");
                        Console.ReadLine();
                        Console.WriteLine("地面から雑草が生える風景が広がっている。");
                        Console.ReadLine();
                        Console.WriteLine("階段は地上までで、建物に地下は存在しないようだ。");
                        Console.ReadLine();
                        Console.WriteLine("地上についたことで、この建物の外の様子がよくわかるようになったが、ほとんど他の建物はなかった。");
                        Console.ReadLine();
                        Console.WriteLine("広がっていたのは、建物の残骸。それだけだった。");
                        Console.ReadLine();
                        Console.WriteLine("しかし、やや離れた場所に1つだけぽつんと建物があるように見えた。");
                        Console.ReadLine();
                        Console.WriteLine("建物に近づいてみることにした。");
                        Console.ReadLine();
                        Console.WriteLine("その建物は、周りの建物の残骸に比べるとかなりましだが、それでもかなり損傷があり、今にも崩れそうだ。");
                        Console.ReadLine();
                        Console.WriteLine("また、白い壁と蒼い屋根を基調とし、窓がいくつかついていた。");
                        Console.ReadLine();
                        Console.WriteLine("窓…といっても窓自体はほとんど割れていて、その残骸と窓枠だけが残っているのだが。");
                        Console.ReadLine();
                        Console.WriteLine("家というには外から見てもあまりに狭く、一部屋分くらいの広さしかないように見えた。");
                        Console.ReadLine();
                        Console.WriteLine("窓から中の様子を覗いてみても、中に人影や生物の姿は見えなかった。");
                        Console.ReadLine();
                        Console.WriteLine("「誰もいないのか…」");
                        Console.ReadLine();
                        Console.WriteLine("安全なことを確認できたため、中に実際に入ることにした。");
                        Console.ReadLine();
                        Console.WriteLine("中には、小さな本棚が壁側に、それと机と椅子があった。");
                        Console.ReadLine();
                        // 探索ターン
                        // 最短√…机のメモを読む→本棚からメモに書いてある題名の本を探す→斜め読み→地上世界へ戻る魔法→終了(神には遭遇しない)
                        bool which = false;
                        bool note = false;
                        while (which == false)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("どこを調べますか");
                            Console.WriteLine("1:本棚");
                            Console.WriteLine("2:机");
                            Console.WriteLine("3:椅子");

                            command = Console.ReadLine();
                            switch (command)
                            {
                                case "1":
                                    Console.WriteLine("20冊以上の本が並んでいた。");
                                    Console.ReadLine();
                                    // 机のメモ帳を見ているか見ていないか
                                    if (note == false)
                                    {
                                        // 机のメモ帳を見ていないときの処理
                                        Console.WriteLine("右から1冊取り、読んでみたがいまいち意味が分からなかった。");
                                        Console.ReadLine();
                                    }
                                    else
                                    {
                                        // 机のメモ帳を見ているときの処理
                                        Console.WriteLine("その中には、先ほど机上のメモに書かれていた「天界のレートピシ」という題名の本があった。");
                                        Console.ReadLine();
                                        Console.WriteLine("手に取り、近年の部分を斜め読みしてみることにした。");
                                        Console.ReadLine();
                                        Console.WriteLine("");
                                        Console.WriteLine(" - 天界のレートピシ - ");
                                        Console.ReadLine();
                                        Console.WriteLine("xx32年、天界に黒い魔物が現れた。");
                                        Console.WriteLine("至急、防衛隊が駆けつけ対処できたものの、原因の究明が課題となっている。");
                                        Console.ReadLine();
                                        Console.WriteLine("xx35年、天界にて黒い魔物は増え続けた。");
                                        Console.WriteLine("原因は、地上との通信の際に誤って移送空間ができたことだといわれている。");
                                        Console.ReadLine();
                                        Console.WriteLine("xx36年、天界西地域を中心として調査が進み、移送空間の場所の特定ができた。");
                                        Console.WriteLine("しかし、まだ数か所は発見できておらず、そこから侵入した魔物が新しい移送空間を設立し続けている。");
                                        Console.ReadLine();
                                        Console.WriteLine("xx40年、天界全域にてほとんどの移送空間の破壊が完了した。");
                                        Console.WriteLine("ただし、天界の主要な地域はほとんど魔物によって建築物が破壊されている。");
                                        Console.ReadLine();
                                        Console.WriteLine("xx42年、主要地域にて都市の修復を開始。");
                                        Console.WriteLine("また、地上の主要国家との連携もあり、天界中心都市付近の移送空間が完全に破壊されたことを報告する。");
                                        Console.ReadLine();
                                        Console.WriteLine("xxxx年、地上国家の戦争によって支援を受けられなくなった。");
                                        Console.WriteLine("わずかに残る移送空間からいまだに魔物の進行が進んでいる。");
                                        Console.WriteLine("");
                                        Console.WriteLine(" - - - - - ");
                                        Console.WriteLine("");
                                        Console.ReadLine();
                                        Console.WriteLine("読み終わると、メモが落ちてきた。");
                                        Console.ReadLine();
                                        Console.WriteLine("メモは丁寧な文字で、次のように書かれていた。");
                                        Console.ReadLine();
                                        Console.WriteLine("「地上世界へ逃げる方法…ここで天井を見て右に1回転、下を向いて左に1回転する」");
                                        Console.ReadLine();
                                        Console.WriteLine("書かれているようにする。");
                                        Console.ReadLine();
                                        Console.WriteLine("すると、視界は暖かい白の光に覆われた。");
                                        Console.ReadLine();
                                        Console.WriteLine("そのような状態で何秒か経つと、だんだんとその白い光は薄くなっていった。");
                                        Console.ReadLine();
                                        Console.WriteLine("気が付くと、目の前には白樺の樹林、雑草の中に見覚えのある秘色と紅の花々が咲き乱れていた。");
                                        Console.ReadLine();
                                        Console.WriteLine("「戻れたのね…」");
                                        Console.ReadLine();
                                        Console.WriteLine("少し左を向いて先ほどのことを思い出す。");
                                        Console.ReadLine();
                                        Console.WriteLine("「何だったのから…さっきのは。書物には天界って書いてあったけど。」");
                                        Console.ReadLine();
                                        Console.WriteLine("先ほどの記憶は確かにあるが、現実的にそんなことがありうるとは思えなかった。");
                                        Console.ReadLine();
                                        Console.WriteLine("論理的に証明できない不安感を忘れるため、先ほどの現実は忘れることにした。");
                                        Console.ReadLine();
                                        Console.WriteLine("秋月は、更なる森の探検へと足を動かした。");
                                        Console.ReadLine();
                                        which = true;
                                    }
                                    break;

                                case "2":
                                    Console.WriteLine("白い塗装が施されている机だ。");
                                    Console.ReadLine();
                                    Console.WriteLine("机上にはメモ帳と、ペンが1本ある。");
                                    Console.ReadLine();
                                    Console.WriteLine("メモ帳には次の文字が殴り書きされていた。");
                                    Console.ReadLine();
                                    Console.WriteLine("「天界のレートピシ」"); // レートピシ=歴史書
                                    Console.ReadLine();
                                    note = true;
                                    break;

                                case "3":
                                    Console.WriteLine("何一つ変哲のない木製の椅子だ。");
                                    Console.ReadLine();
                                    Console.WriteLine("座る部分は水色の布に白い糸で刺繍がされている。");
                                    Console.ReadLine();
                                    break;

                                default:
                                    InvalidInput();
                                    break;
                            }
                        }
                        Console.WriteLine("");
                        Console.WriteLine("END:小屋の本(通常ルート2)");
                        Console.WriteLine("[真実を記す手記]");
                        Console.WriteLine("");
                        Console.ReadLine();
                        break;

                    case "3":
                        where = true;
                        Console.WriteLine("彼女は、階段の逆の方向を向いた。");
                        Console.ReadLine();
                        Console.WriteLine("「もう、疲れたよ…」");
                        Console.ReadLine();
                        Console.WriteLine("すると、走って窓に向かって飛び蹴りを炸裂し、見事にその窓を割って地上へ落ちていった。");
                        Console.ReadLine();
                        Console.WriteLine("今までの記憶をたどる。");
                        Console.ReadLine();
                        Console.WriteLine("家族の事、国の事、かつて見た未来の夢。");
                        Console.ReadLine();
                        Console.WriteLine("昔の戦争で再び会うことを誓った3人の同士、同じ部隊の黒服の少女…");
                        // 昔の戦争 (対チャクム戦/北大陸南西部防衛) での3人の仲間 (「世界の終焉の前に。」)、
                        // 同じ部隊 (第9師団/旧黒軍師) の黒服の少女 (椿と航空隊の2人)
                        Console.ReadLine();
                        Console.WriteLine("様々なことを思い出した。");
                        Console.ReadLine();
                        Console.WriteLine("死ぬ前に会いたかった人がいた。でも、もう戻れない。");
                        Console.ReadLine();
                        Console.WriteLine("「ここからもとに戻れる気がしないんだ…」");
                        Console.ReadLine();
                        Console.WriteLine("「でも、もう一度でいいから見たかった…」");
                        // 家族と、旧黒軍師メンバー、かつての仲間 (「世界の終焉の前に。」メンバー)、第9師団の上司と。
                        Console.ReadLine();
                        Console.WriteLine("その瞳に映るのは真っ青な空と、白い雲だけだった。");
                        Console.ReadLine();
                        Console.WriteLine("「あなた…こっちの世界に迷い込んでしまったのかしら…」");//神
                        Console.ReadLine();
                        Console.WriteLine("見知らぬ声が聞こえる。");
                        Console.ReadLine();
                        Console.WriteLine("「ええと、」");//神
                        Console.ReadLine();
                        Console.WriteLine("その声の後、体がふわっと浮いたような感じがした。");
                        Console.ReadLine();
                        Console.WriteLine("視界は真っ白になり、ちょうど地下からこちらに来た時と同じように暖かさのある光だった。");
                        Console.ReadLine();
                        Console.WriteLine("瞬きをした次の瞬間、私の視界は緑色に支配されていた。");
                        Console.ReadLine();
                        Console.WriteLine("鬱蒼と茂る白樺並木、背丈の低い雑草と、まばらに生える秘色と紅の花。");
                        Console.ReadLine();
                        Console.WriteLine("間違いなく始めの森に戻ったようだ。");
                        Console.ReadLine();
                        Console.WriteLine("「あなたはこちらの人間のようね…」");//神
                        Console.ReadLine();
                        Console.WriteLine("声の聞こえる後ろを振り返ると、そこには長髪で神々しさをまとう人の姿があった。");
                        Console.ReadLine();
                        Console.WriteLine("「ええと、あなたは…」");//秋月
                        Console.ReadLine();
                        Console.WriteLine("そう聞くと、その言葉を遮るように彼女は次の言葉を続けた。");
                        Console.ReadLine();
                        Console.WriteLine("「こちらはあなたが来ていい世界ではないわ。」");//神
                        Console.ReadLine();
                        Console.WriteLine("「こちらのミスで来てしまったみたいだけれど。申し訳ないわ…」");//神
                        Console.ReadLine();
                        Console.WriteLine("意味が分からず、質問をしようとした瞬間、彼女の姿はなくなっていた。");
                        Console.ReadLine();
                        Console.WriteLine("私の視界にはただ森の風景と彼女が先ほどいた場所にある透明な泉のみが残った。");
                        Console.ReadLine();
                        Console.WriteLine("ただ、地上に戻れたという安堵の気持ちが湧き出て、近くにあった木に倒れこんだ。");
                        Console.ReadLine();
                        Console.WriteLine("");
                        Console.WriteLine("END:森の泉(隠しルート)");
                        Console.WriteLine("[秋月の過去と見えない事実]");
                        // 見えない事実(現実)…泉にいる神の言っていることが意味不明&説明不足過ぎる
                        Console.WriteLine("");
                        Console.ReadLine();
                        break;

                    case "4":
                        Save();
                        break;

                    default:
                        InvalidInput();
                        break;
                }
            }

            // 運が若干高い場合は秋月と椿の会話について解放
            // この文章はぶっちゃけこのゲーム内でも、Ivy Cafeteria のシナリオを知っている人でもわからないと思う。
            // (親友=椿とわかる人はすごい。親友→×親、上司、△世界の終焉の前に。(長い間合っていないので一緒に喋る機会がない)、〇旧黒軍師
            // →旧黒軍師では3人いるが、航空隊2人の名前や詳細がまだ決まっていないため、椿という推測は一応可能だが。
            // (未来になって航空隊2人の名前が決まっても、決まっていなかった時代のストーリーということで未来になっても上記と同じ考察が可能。))
            if (komariLack >= 8)
            {
                Console.WriteLine(" - - - - - ");
                Console.WriteLine("「っていうことがあったのよね」");//秋月
                Console.ReadLine();
                Console.WriteLine("後日、次の目的地までの移動中に親友と話をしていた。");
                Console.ReadLine();
                Console.WriteLine("「へえ…珍しいこともあるんだね。」");//椿
                Console.ReadLine();
                Console.WriteLine("「あ、言っておくけど、作り話じゃなくて本当だからね!」");//秋月
                Console.ReadLine();
                Console.WriteLine("「わかってるよ。それより…」");//椿
                Console.ReadLine();
                Console.WriteLine("「今日の計画でしょ。」");//秋月
                Console.ReadLine();
                Console.WriteLine("持っている小銃を傾けた。");//秋月
                Console.ReadLine();
                Console.WriteLine("「そうね。」");//椿
                Console.ReadLine();
                Console.WriteLine("「今回は、未探索地域の調査だってね。」");//秋月
                Console.ReadLine();
                Console.WriteLine("「ふうん…楽しみね。」");//椿
                Console.ReadLine();
                Console.WriteLine("「そうだね。」");//秋月
                // 一応A-RPGの布石。A-RPGの中でも電車が居なくなって、暇だからトンネルに入るみたいな話のやつ。
                // 内容とか導入が被っているからあのシナリオは使わないかもしれないけど…
                // あと、あれは3Dでやる予定だったから1人用(秋月のみの)シナリオだし…
                // ここの会話内容は後で変えたほうがいいかも…(フラグを残せる重要な場所なので。)
                Console.WriteLine(" - - - - - ");
                Console.WriteLine("");
            }

            Console.WriteLine("開発");
            Console.WriteLine("プログラム: Lemon73 (Ivy Cafeteria)");
            Console.WriteLine("シナリオ: Lemon73 (Ivy Cafeteria)");
            Console.WriteLine("");
            Console.WriteLine("ベース: .NET 8.0 (Console) (Microsoft)");
            Console.WriteLine("");
            Console.WriteLine("終了です。お疲れさまでした。");
            Console.ReadLine();
            // 裏話…手榴弾を実装したのは、実はトンネル内にふさがっているところがあって、そこを爆破して進んでいくためだったんだけど、
            // トンネルの話が長くなりすぎてつまらなかったから辞めた。だから、攻撃と逃げるときの両方で使えるくらいしかメリットはない
            // っていうか、手榴弾はともかく、銃の弾丸は普通に余るな…
            // 医療品なんかそもそも使わなくても魔法回復できるし…(戦闘前のMP回復のおかげでほぼMP切れにならないし)
            // 難易度調整はミスった(簡単すぎた)かも(だからといって今から調整するのも面倒)
            break;

        case "2":
            try
            {
                Load();
                switch (section)
                {
                    case 1: goto label1;
                    case 2: goto label2;
                    case 3: goto label3;
                    case 6: goto label4c;
                }
                playing = false;
                Console.WriteLine("ロードが成功しました。");
                Console.ReadLine();
            }
            catch
            {
                Console.WriteLine("セーブデータが見つかりません。");
                Console.WriteLine("");
            }
            break;

        case "3":
            playing = false;
            break;

        default:
            InvalidInput();
            break;
    }
}

// 誤った入力
void InvalidInput()
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Error.WriteLine("誤ったコマンドを入力しています。");
    Console.ResetColor();
}

// 警告文字
void WarningConsole(string message)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(message);
    Console.ReadLine();
    Console.ResetColor();
}

// セーブ処理
void Save()
{
    //データ保存(xml)
    XElement element = new
        (
            "Data",
            new XElement("Section", section),
            new XElement("KomariHP", komariHP),
            new XElement("KomariMP", komariMP),
            new XElement("KomariLuck", komariLack),
            new XElement("Item", item),
            new XElement("ItemBom", itemBom),
            new XElement("ItemBullet", itemBullet),
            new XElement("ItemMedicine", itemMedicine)
        );
    element.Save("Data.xml");
    Console.WriteLine("セーブが完了しました。");
    Console.WriteLine("終了する際は、Ctrl+Cを押してください。");
}

// ロード処理
void Load()
{
    //ファイル読み込み
    XDocument dataLoading = XDocument.Load(@"./Data.xml");

    IEnumerable<XElement> datas = dataLoading.Elements("Data");
    foreach (XElement data in datas)
    {
        section         = int.TryParse(data.Element("Section")?.Value, out var tempSection) ? tempSection : 0;
        komariHP        = int.TryParse(data.Element("KomariHP")?.Value, out int tempKomariHP) ? tempKomariHP : 200;
        komariMP        = int.TryParse(data.Element("KomariMP")?.Value, out int tempKomariMP) ? tempKomariMP : 20;
        komariLack      = int.TryParse(data.Element("KomariLuck")?.Value, out int tempKomariLack) ? tempKomariLack : 10;
        item            = int.TryParse(data.Element("Item")?.Value, out int tempItem) ? tempItem : 0;
        itemBom         = int.TryParse(data.Element("ItemBom")?.Value, out int tempItemBom) ? tempItemBom : 2;
        itemBullet      = int.TryParse(data.Element("ItemBullet")?.Value, out int tempItemBullet) ? tempItemBullet : 4;
        itemMedicine    = int.TryParse(data.Element("ItemMedicine")?.Value, out int tempItemMedicine) ? tempItemMedicine : 4;
    }
}

//戦闘処理
void Buttle()
{
    Console.WriteLine(" -+-+- 戦闘開始 -+-+- ");
    Console.ReadLine();

    Random rand = new();

    int enemyDefaultHP = 100 + rand.Next(0, 15) * 10; // 100-240 (10刻み)
    const string enemyDefaultName = "異形の存在";

    // HP が設定されていないときはランダム値に設定
    enemyHP = (enemyHP > 0) ? enemyHP : enemyDefaultHP;
    // 名前が設定されていないときは初期値に設定
    enemyName = (enemyName == "") ? enemyName : enemyDefaultName;

    // 戦闘開始時自動回復 (難易度調節)
    komariHP += 20 + rand.Next(0, 3) * 10; // 20-40 (10刻み)
    komariMP += rand.Next(2, 6); // 2-5

    while (enemyHP > 0)
    {
        Console.WriteLine(" - あなたのターン - ");
        Console.ReadLine();

        //キャラクターステータス表示
        Console.WriteLine("");
        Console.WriteLine("味方ステータス");
        Console.WriteLine("|秋月小鞠|HP:{0}|MP:{1}|", komariHP, komariMP);
        Console.WriteLine("");
        Console.WriteLine("敵ステータス");
        Console.WriteLine("|{0}|HP:{1}|", enemyName, enemyHP);

        //作成途中
        bool where = false;
        while (where == false)
        {

            Console.WriteLine("");
            Console.WriteLine("どのような行動をしますか");
            Console.WriteLine("1:ナイフ攻撃");
            Console.WriteLine("2:手榴弾攻撃(残り{0}個)", itemBom);
            Console.WriteLine("3:拳銃射撃(残り{0}発)", itemBullet);
            Console.WriteLine("4:治療魔法");//MPを使って回復
            Console.WriteLine("5:回復薬治療(残り{0}個)", itemMedicine);
            Console.WriteLine("6:戦略的撤退");//逃げる
            Console.WriteLine("7:手榴弾退散(残り{0}個)", itemBom);//手榴弾の爆破と同時に逃げることで「戦略的撤退」よりも高確率で逃げ切れる
            Console.WriteLine("8:敵味方のステータスを再確認");

            command = Console.ReadLine();
            switch (command)
            {
                case "1":
                    where = true;
                    //近接戦闘
                    Console.WriteLine("敵に接近して、ナイフを振り上げて…");
                    Console.WriteLine("その腕を素早く振り降ろした。");
                    Console.ReadLine();
                    Console.WriteLine("敵はダメージを受けたようだ。");
                    Console.ReadLine();
                    enemyHP -= 50;//固定ダメージ
                    break;

                case "2":
                    //爆破攻撃
                    if (itemBom > 0)
                    {
                        where = true;
                        itemBom -= 1;
                        Console.WriteLine("秋月は後ろずさりで少しずつ後退しながら、手榴弾のピンを抜いて、");
                        Console.WriteLine("敵のほうに向かって投げつけた。");
                        Console.ReadLine();
                        Console.WriteLine("しばらくすると、大きな爆発音を上げ、付近は白い煙に包まれた。");
                        Console.ReadLine();
                        enemyHP -= 150 + rand.Next(0, 4) * 10;//150~180(10刻み)
                    }
                    else
                        WarningConsole("爆弾がないようだ。");
                    break;

                case "3":
                    //射撃攻撃処理
                    //(ナイフ/爆弾との違いのために命中率は低く、ダメージが大きい、貫通するとさらにダメージ増加とかのほうがいいかも。
                    //↑一応、弾薬数が多いため攻撃可能回数がほぼ無限というメリットもあるけど。)
                    if (itemBullet > 0)
                    {
                        where = true;
                        itemBullet -= 1;
                        Console.WriteLine("拳銃に弾丸を込め、単発射撃を行った。");
                        Console.ReadLine();
                        Console.WriteLine("拳銃は、爆音を上げて、強い反動を受けた。");
                        Console.ReadLine();
                        Console.WriteLine("弾丸は敵を直撃して、敵の体を貫通した。");
                        Console.ReadLine();
                        enemyHP -= 40 + rand.Next(0, 27) * 10;// 40~300(10刻み)
                    }
                    else
                        WarningConsole("弾薬が足りないようだ。");
                    break;

                case "4":
                    //治療処理
                    where = true;
                    if (komariMP >= 3)
                    {
                        komariMP -= 3;
                        Console.WriteLine("「リヴァレド・ハート」");
                        Console.WriteLine("両手を空にかざし、回復魔法を唱えた。");
                        Console.ReadLine();
                        Console.WriteLine("優しいオーラが秋月の体を包み込む。");
                        Console.ReadLine();
                        Console.WriteLine("傷は見る見るうちに回復していった。");
                        Console.ReadLine();
                        komariHP += 150;//固定
                    }
                    else
                    {
                        Console.WriteLine("魔力が足りないため、簡易的な治療のみ行うことにした。");
                        Console.ReadLine();
                        Console.WriteLine("ナイフでスカートの先を切り、その布で傷ついた部分を保護した。");
                        Console.ReadLine();
                        komariHP += 40;//固定
                    }

                    break;

                case "5":
                    //アイテム治療処理
                    if (itemMedicine > 0)
                    {
                        where = true;
                        itemMedicine -= 1;
                        Console.WriteLine("医療品をあさり、治療に使えそうな薬品などを取り出した。");
                        Console.ReadLine();
                        Console.WriteLine("傷ついた部分に治療薬を塗り、包帯で巻いて痛みを和らげることができた。");
                        Console.ReadLine();
                        komariHP += 220;//固定
                    }
                    else
                        WarningConsole("治療薬がないようだ。");
                    break;

                case "6":
                    where = true;
                    //確立で逃げる処理(20%の確率で逃げられる)
                    if (rand.Next(0, 101) <= 20)
                    {
                        Console.WriteLine("敵からなんとか逃げ切ることができた。");
                        Console.ReadLine();
                        enemyHP = 0;
                    }
                    else
                        WarningConsole("敵に回り込まれた。");
                    break;

                case "7":
                    //手榴弾があるかを確認
                    if (itemBom > 0)
                    {
                        where = true;
                        itemBom -= 1;
                        Console.WriteLine("秋月は後ろずさりで少しずつ後退しながら、手榴弾のピンを抜いて、");
                        Console.WriteLine("明後日のほうに向かって投げつけた。");
                        Console.ReadLine();
                        Console.WriteLine("しばらくすると、大きな爆発音を上げ、付近は白い煙に包まれた。");
                        Console.ReadLine();
                        Console.WriteLine("煙に紛れて逃げようと試みる。");
                        Console.ReadLine();

                        //確立で逃げる処理(60%の確率で逃げられる)
                        if (rand.Next(0, 101) <= 60)
                        {
                            Console.WriteLine("なんとか逃げ切ることができた。");
                            Console.ReadLine();
                            enemyHP = 0;
                        }
                        else
                            WarningConsole("しかし、敵に回り込まれてしまった。"); // 敵にダメージはなし
                    }
                    else
                        WarningConsole("爆弾がないようだ。");
                    break;

                case "8":
                    Console.WriteLine("");
                    Console.WriteLine("味方ステータス");
                    Console.WriteLine("|秋月小鞠|HP:{0}|MP:{1}|", komariHP, komariMP);
                    Console.WriteLine("");
                    Console.WriteLine("敵ステータス");
                    Console.WriteLine("|{0}|HP:{1}|", enemyName, enemyHP);
                    break;

                default:
                    InvalidInput();
                    break;
            }

            //敵死亡判定
            if (enemyHP <= 0)
            {
                switch (command)
                {
                    case "6":
                    case "7":
                        break;

                    default:
                        //逃げた場合以外表示
                        Console.WriteLine("敵はその場に倒れ、もう二度と動かなくなった。");
                        Console.ReadLine();
                        break;
                }
                enemyName = "";//敵の名前を未指定状態にする
                Console.WriteLine(" -+-+- 戦闘終了 -+-+- ");
                Console.ReadLine();
                Console.WriteLine("「ふう。何とかなったわね。」");
                Console.ReadLine();
                Console.WriteLine("「先へ進むわ。」");
                Console.ReadLine();
            }
            else
            {
                // もし攻撃or回復(逃げる+誤入力以外)なら
                // このif文で↓のバグを回避できる。
                // (8の「様子を見る」や誤コマンドの際に本来なら再度コマンドを選べるはずなのに、すぐに敵ターンに移行してしまうバグ。
                // 原因:whileの中に敵の処理を入れているから)
                if (where == true)
                {
                    // 続行 (敵ターン)
                    Console.WriteLine(" - 敵のターン - ");
                    Console.ReadLine();

                    // 70%
                    if (rand.Next(10) < 7)
                    {
                        Console.WriteLine("敵からの攻撃");
                        Console.ReadLine();
                        komariHP -= rand.Next(5, 14) * 10;//50~130(10刻み)(最大値が出るとまあまあ強いのでHPに注意。)
                    }
                    // 20%
                    else if (rand.Next(3) != 0)
                    {
                        Console.WriteLine("敵は様子を見ている。");
                        Console.ReadLine();
                    }
                    // 10%
                    else
                    {
                        Console.WriteLine("敵は回復魔法を唱えた。");
                        Console.ReadLine();
                        enemyHP += 120; // 固定
                    }

                    // 秋月死亡判定
                    if (komariHP <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red; // 文字の色を変更
                        Console.WriteLine("");
                        Console.WriteLine("「う、うそ…」");
                        Console.ReadLine();
                        Console.WriteLine("体に大きな傷を負った秋月はもう、そこから動くことができなくなってしまった。");
                        Console.ReadLine();
                        Console.WriteLine("視界に広がる真紅の液体。それが自分の運命を示唆しているようだった。");
                        Console.ReadLine();
                        Console.WriteLine("「こ…こんなところで死ぬはずは…」");
                        Console.ReadLine();
                        Console.WriteLine("言葉を言い終える前に、その人生の終わりを早々と迎えた。");
                        Console.ReadLine();
                        Console.ResetColor(); // 色をリセット
                        // 終了判定
                        enemyHP = 0;
                        Environment.Exit(0);
                    }
                }
            }
        }
    }
}
