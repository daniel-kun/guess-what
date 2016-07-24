using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace Io.GuessWhat.Playground
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class Program
    {
        public static int Main(string[] args)
        {
            var client = new MongoClient("");
            var db = client.GetDatabase("guess-what");
            var templateCollection = db.GetCollection<BsonDocument>("templates");
            // Use this to delete single items or spam:

            // templateCollection.DeleteOne(new BsonDocument(new BsonElement("_id", BsonValue.Create("Zp-ifMYR_0W6D0GwKsuoSw"))));
            // templateCollection.DeleteOne(new BsonDocument(new BsonElement("_id", BsonValue.Create("Ss0ISYQbSEyUsYVnMjbDAw"))));
            
            /*
            var regex = new Regex("^<a href=\"http://.*#\\d*\">.*</a>,$");
            foreach (var template in templateCollection.Find(new BsonDocument()).ToList())
            {
                var description = template.GetElement("Description").Value.ToString().Trim();
                if (regex.Matches(description).Count > 0)
                {
                    templateCollection.DeleteOne(new BsonDocument (template.GetElement("_id")));
                }
                else
                {
                    Console.WriteLine("OK  :" + description);
                }
            }
            */
            Console.WriteLine("END.");
            Console.ReadLine();
            return 0;
        }
    }
}

/*
Here are some spam samples:
<a href="http://buytrazodone.pro/#3615">trazodone</a>,
<a href="http://buy-glucophage.kim/#6979">glucophage</a>,
<a href="http://viagraprice.bid/#6340">generic viagra price</a>,
<a href="http://amitriptyline-10mg.party/#1185">amitriptyline</a>,
<a href="http://singulair10mg.date/#5499">singulair 10mg</a>,
<a href="http://atenolol-25-mg.party/#3958">atenolol 25 mg</a>,
<a href="http://buy-robaxin.kim/#2610">buy robaxin</a>,
<a href="http://albendazole.stream/#4308">albendazole</a>,
<a href="http://genericwellbutrin.eu/#8834">wellbutrin</a>,
<a href="http://buymethotrexate.pro/#8797">generic methotrexate</a>,
<a href="http://viagra-generic.gdn/#1527">viagra</a>,
<a href="http://citalopram-20mg.webcam/#4180">citalopram</a>,
<a href="http://clomidformen.xyz/#2224">how to get clomid</a>,
<a href="http://tamoxifen-citrate.top/#7736">tamoxifen</a>,
<a href="http://celebrex-online.party/#4011">celebrex</a>,
<a href="http://buy-clomid.bid/#6705">where to buy clomid online</a>,
<a href="http://viagraoverthecounter.date/#3510">viagra over the counter</a>,
<a href="http://buy-tetracycline.webcam/#5550">tetracycline online no prescription</a>,
<a href="http://buy-nexium.trade/#7211">nexium</a>,
<a href="http://cytotec-online.xyz/#2456">cytotec online</a>,
<a href="http://buy-cephalexin.date/#7925">cephalexin</a>,
<a href="http://diclofenacsodec.party/#9997">our website</a>,
<a href="http://buy-strattera.kim/#8364">strattera online</a>,
<a href="http://buy-clomid.stream/#4998">our website</a>,
<a href="http://buy-acyclovir.kim/#6520">buy acyclovir</a>,
<a href="http://cipro-antibiotic.webcam/#1205">cipro antibiotic</a>,
<a href="http://buy-hydrochlorothiazide.date/#7261">hydrochlorothiazide</a>,
<a href="http://wellbutrinonline.cricket/#7405">purchase wellbutrin online</a>,
<a href="http://citalopram-hbr-20-mg.gdn/#3311">citalopram hbr 20 mg</a>,
<a href="http://seroquel-online.science/#5862">seroquel</a>,
<a href="http://buypropecia.link/#6132">propecia</a>,
<a href="http://genericforzoloft.gdn/#9656">zoloft</a>,
<a href="http://cialis-price.top/#4536">cialis</a>,
<a href="http://zovirax-cream.trade/#6033">acyclovir 500 mg</a>,
<a href="http://prednisoloneacetate.trade/#5671">prednisolone sodium</a>,
<a href="http://cymbalta-generic.cricket/#9750">cymbalta online</a>,
<a href="http://bupropiononline.date/#2733">bupropion</a>,
<a href="http://buy-avodart.kim/#6786">avodart</a>,
<a href="http://citalopram10mg.cricket/#1894">citalopram 10 mg tablet</a>,
<a href="http://buy-rimonabant.date/#9642">rimonabant acomplia</a>,
<a href="http://methylprednisolone.top/#6756">here</a>,
<a href="http://buy-wellbutrin.bid/#1423">wellbutrin</a>,
<a href="http://retin-a-micro-gel.top/#9189">retin-a micro gel</a>,
<a href="http://rogaine-for-men.trade/#1450">rogaine</a>,
<a href="http://buy-effexor.cricket/#5267">buy effexor xr 150mg</a>,
<a href="http://singulair10mg.top/#2898">singulair</a>,
<a href="http://neurontin.pro/#9190">neurontin</a>,
<a href="http://cialis-generic.trade/#8264">cialis-generic</a>,
<a href="http://buy-diclofenac.link/#9681">diclofenac</a>,*/
